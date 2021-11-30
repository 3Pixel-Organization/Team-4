using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CustomSceneManagement.Core.Worker.Instructions;
using CustomSceneManagement.Core.Worker.Runtimes;

namespace CustomSceneManagement.Core.Worker
{
	public class SceneWorker : MonoBehaviour
	{
		private List<WorkerInstruction> workerInstructions = new List<WorkerInstruction>();
		private int instructionIndex = 0;
		private WorkerRuntime activeRuntime;
		private bool isDone = false;

		public void AssignInstructions(List<WorkerInstruction> workerInstructions)
		{
			this.workerInstructions = workerInstructions;
		}

		public void StartWork()
		{
			instructionIndex = -1;
			NextStep();
		}

		private void NextStep()
		{
			instructionIndex++;
			if(instructionIndex < workerInstructions.Count)
			{
				WorkerInstruction workerInstruction = workerInstructions[instructionIndex];

				if (workerInstruction is SceneOPInstruction sceneInstruction)
				{
					activeRuntime = new SceneOPRuntime(NextStep, sceneInstruction);
				}
				else if (workerInstruction is MultiSceneOPInstruction multiSceneInstruction)
				{
					activeRuntime = new MultiSceneOPRuntime(NextStep, multiSceneInstruction);
				}
				else if (workerInstruction is LoadingScreenOPInstruction loadingScreenInstruction)
				{
					activeRuntime = new LoadingScreenOPRuntime(NextStep, loadingScreenInstruction);
				}
				else if (workerInstruction is DelayOPInstruction delayInstruction)
				{
					activeRuntime = new WorkerRuntime(null);
					Invoke(nameof(NextStep), delayInstruction.TimeDelay);
				}
			}
			if(instructionIndex == workerInstructions.Count)
			{
				FinishUp();
			}
		}

		private void FinishUp()
		{
			Destroy(gameObject, 0.1f);
			isDone = true;
			SceneDirector.WorkerComplete();
		}

		// Start is called before the first frame update
		void Start()
		{
			activeRuntime = new WorkerRuntime(NextStep);
			DontDestroyOnLoad(gameObject);
		}

		// Update is called once per frame
		void Update()
		{
			//Debug.Log(TotalProgress());
		}

		public float TotalProgress()
		{
			if(isDone) return 1;

			if(workerInstructions.Count == 0)
			{
				return 0;
			}
			else
			{
				return (activeRuntime.Progress() + instructionIndex) / workerInstructions.Count;
			}
			
		}
	}
}
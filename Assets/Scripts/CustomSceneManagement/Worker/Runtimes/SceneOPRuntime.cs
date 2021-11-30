using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomSceneManagement.Core.Worker.Instructions;

namespace CustomSceneManagement.Core.Worker.Runtimes
{
	public class SceneOPRuntime : WorkerRuntime
	{
		private SceneOPInstruction instruction;
		public AsyncOperation asyncOperation;

		public SceneOPRuntime(Action callback, SceneOPInstruction sceneOPInstruction) : base(callback)
		{
			instruction = sceneOPInstruction;
			if (instruction.Behavior == SceneBehavior.Unload)
			{
				asyncOperation = SceneManager.UnloadSceneAsync(instruction.SceneData.SceneName);
				asyncOperation.completed += AsyncOperation_completed;
			}
			else if (instruction.Behavior == SceneBehavior.Reload)
			{
				new SceneOPRuntime(LoadUnloadedScene, new SceneOPInstruction(SceneBehavior.Unload, instruction.SceneData));
			}
			else if(instruction.Behavior == SceneBehavior.Load)
			{
				asyncOperation = SceneManager.LoadSceneAsync(instruction.SceneData.SceneName, LoadSceneMode.Additive);
				//asyncOperation.allowSceneActivation = false;
				asyncOperation.completed += AsyncOperation_completed;
			}
			else
			{
				callback?.Invoke();
			}
		}

		
		//public float Progress => asyncOperation.progress;

		public override float Progress()
		{
			if (isDone)
				return 1;
			if(asyncOperation != null)
			{
				return asyncOperation.progress;
			}
			return 0;
		}

		private void AsyncOperation_completed(AsyncOperation obj)
		{
			Complete();
		}

		private void LoadUnloadedScene()
		{
			new SceneOPRuntime(Complete, new SceneOPInstruction(SceneBehavior.Load, instruction.SceneData));
		}
	}
}
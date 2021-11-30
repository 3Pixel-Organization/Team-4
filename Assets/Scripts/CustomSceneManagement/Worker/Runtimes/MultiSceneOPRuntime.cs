using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomSceneManagement.Core.Worker.Instructions;

namespace CustomSceneManagement.Core.Worker.Runtimes
{
	class MultiSceneOPRuntime : WorkerRuntime
	{
		MultiSceneOPInstruction instruction;

		List<SceneOPRuntime> sceneOPRuntimes = new List<SceneOPRuntime>();

		public MultiSceneOPRuntime(Action callback, MultiSceneOPInstruction instruction) : base(callback)
		{
			this.instruction = instruction;
			
			foreach (SceneOPInstruction item in instruction.SceneOPInstructions)
			{
				sceneOPRuntimes.Add(new SceneOPRuntime(Update, item));
			}
		}

		private void Update()
		{
			foreach (SceneOPRuntime item in sceneOPRuntimes)
			{
				if (!item.IsDone)
					return;
			}

			foreach (SceneOPRuntime item in sceneOPRuntimes)
			{
				item.asyncOperation.allowSceneActivation = true;
			}
			Complete();
		}

		public override float Progress()
		{
			if (isDone)
				return 1;

			float buffer = 0;
			foreach (SceneOPRuntime item in sceneOPRuntimes)
			{
				buffer += item.Progress();
			}
			buffer /= sceneOPRuntimes.Count;
			return buffer;
		}
	}
}

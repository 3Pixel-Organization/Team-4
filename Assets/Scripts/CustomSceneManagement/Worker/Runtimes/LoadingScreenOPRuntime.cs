using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomSceneManagement.Core.Worker.Instructions;

namespace CustomSceneManagement.Core.Worker.Runtimes
{
	class LoadingScreenOPRuntime : WorkerRuntime
	{
		LoadingScreenOPInstruction instruction;
		public LoadingScreenOPRuntime(Action callback, LoadingScreenOPInstruction instruction) : base(callback)
		{
			this.instruction = instruction;
			switch (instruction.Behavior)
			{
				case LoadingScreenBehavior.Start:
					Start();
					break;
				case LoadingScreenBehavior.Stop:
					Stop();
					break;
				default:
					break;
			}
		}

		void Start()
		{
			if (instruction.SceneData.IsLoaded)
			{
				ActivateLoadingScene();
			}
			else
			{
				new SceneOPRuntime(ActivateLoadingScene, new SceneOPInstruction(SceneBehavior.Load, instruction.SceneData));
			}

			void ActivateLoadingScene()
			{
				if (instruction.SceneData.IsLoaded)
				{
					GameObject[] gameObjects = instruction.SceneData.GetScene.GetRootGameObjects();
					for (int i = 0; i < gameObjects.Length; i++)
					{
						if (gameObjects[i].TryGetComponent(out LoadingScreenUI loadingScreenUI))
						{
							loadingScreenUI.Activate();
						}
					}
				}
				Complete();
			}
		}

		void Stop()
		{
			if (instruction.SceneData.IsLoaded)
			{
				GameObject[] gameObjects = instruction.SceneData.GetScene.GetRootGameObjects();
				for (int i = 0; i < gameObjects.Length; i++)
				{
					if (gameObjects[i].TryGetComponent(out LoadingScreenUI loadingScreenUI))
					{
						loadingScreenUI.DeActivate();
					}
				}
			}
			Complete();
		}
	}
}

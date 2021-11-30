using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomSceneManagement.Operations
{
	class SceneOperation
	{
		private SceneDataSO scene;
		private event Action Finshed;
		private SceneOperationBehavior operationBehavior;

		private bool isDone = false;
		public bool IsDone => isDone;
		//public float Progress => asyncOperation.progress;

		public SceneOperation(SceneDataSO scene, Action completed, SceneOperationBehavior operationBehavior)
		{
			this.scene = scene;
			this.operationBehavior = operationBehavior;
			Finshed += completed;

			if (operationBehavior == SceneOperationBehavior.Unload)
			{
				AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(scene.SceneName);
				asyncOperation.completed += AsyncOperation_completed;
			}
			else if (operationBehavior == SceneOperationBehavior.Reload)
			{
				SceneOperation operation = new SceneOperation(scene, LoadUnloadedScene, SceneOperationBehavior.Unload);
			}
			else
			{
				SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
				DoneLoading();
			}
			
		}

		private void AsyncOperation_completed(AsyncOperation obj)
		{
			isDone = true;
			Finshed?.Invoke();
		}

		private void LoadUnloadedScene()
		{
			SceneOperation operation = new SceneOperation(scene, DoneLoading, SceneOperationBehavior.Load);
		}

		private void DoneLoading()
		{
			isDone = true;
			Finshed?.Invoke();
		}
	}
}

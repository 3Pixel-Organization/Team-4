using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomSceneManagement.Operations
{
	public class SceneAsyncOperation
	{
		private SceneDataSO scene;
		private event Action Finshed;
		private SceneOperationBehavior operationBehavior;
		private AsyncOperation asyncOperation;

		private bool isDone = false;
		public bool IsDone => isDone;
		//public float Progress => asyncOperation.progress;

		public float Progress
		{
			get
			{
				if (operationBehavior == SceneOperationBehavior.Reload)
				{
					return 1;
				}
				else
				{
					return asyncOperation.progress;
				}
			}
		}

		public SceneAsyncOperation(SceneDataSO scene, Action completed, SceneOperationBehavior operationBehavior)
		{
			this.scene = scene;
			this.operationBehavior = operationBehavior;
			Finshed += completed;

			if (operationBehavior == SceneOperationBehavior.Unload)
			{
				asyncOperation = SceneManager.UnloadSceneAsync(scene.SceneName);
				asyncOperation.completed += AsyncOperation_completed;
			}
			else if (operationBehavior == SceneOperationBehavior.Reload)
			{
				SceneAsyncOperation operation = new SceneAsyncOperation(scene, LoadUnloadedScene, SceneOperationBehavior.Unload);
			}
			else
			{
				asyncOperation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
				asyncOperation.completed += AsyncOperation_completed;
			}
			
		}

		private void AsyncOperation_completed(AsyncOperation obj)
		{
			isDone = true;
			Finshed?.Invoke();
		}

		private void LoadUnloadedScene()
		{
			SceneAsyncOperation operation = new SceneAsyncOperation(scene, DoneLoading, SceneOperationBehavior.Load);
		}

		private void DoneLoading()
		{
			isDone = true;
			Finshed?.Invoke();
		}
	}
}

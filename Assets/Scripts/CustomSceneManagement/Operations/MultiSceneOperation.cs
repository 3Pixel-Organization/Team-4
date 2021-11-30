using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomSceneManagement.Operations
{
	public class MultiSceneOperation
	{
		private event Action nextStepEvent;

		List<SceneAsyncOperation> sceneOperations = new List<SceneAsyncOperation>();

		List<SceneOperationParms> sceneOperationParms;

		public float Progress
		{
			get
			{
				float buffer = 0;
				foreach (SceneAsyncOperation item in sceneOperations)
				{
					buffer += item.Progress;
				}
				buffer /= sceneOperations.Count;
				return buffer;
			}
		}



		public MultiSceneOperation(List<SceneOperationParms> sceneOperationParms, Action complete)
		{
			nextStepEvent += complete;
			this.sceneOperationParms = sceneOperationParms;
		}

		public void Start()
		{
			foreach (SceneOperationParms item in sceneOperationParms)
			{
				sceneOperations.Add(new SceneAsyncOperation(item.scene, Update, item.operationBehavior));
			}
		}

		private void Update()
		{
			foreach (SceneAsyncOperation item in sceneOperations)
			{
				if (!item.IsDone)
					return;
			}
			nextStepEvent?.Invoke();
		}
	}
}

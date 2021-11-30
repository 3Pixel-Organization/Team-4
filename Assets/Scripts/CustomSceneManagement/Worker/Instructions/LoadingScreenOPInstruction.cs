using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core.Worker.Instructions
{
	class LoadingScreenOPInstruction : WorkerInstruction
	{
		private RuntimeSceneData sceneData;
		private LoadingScreenBehavior behavior;

		public LoadingScreenOPInstruction(RuntimeSceneData sceneData, LoadingScreenBehavior behavior)
		{
			this.sceneData = sceneData;
			this.behavior = behavior;
		}

		public RuntimeSceneData SceneData { get => sceneData;}
		public LoadingScreenBehavior Behavior { get => behavior; }
	}
}

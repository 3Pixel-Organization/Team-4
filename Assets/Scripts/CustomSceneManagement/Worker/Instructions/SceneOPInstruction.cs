using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core.Worker.Instructions
{
	public class SceneOPInstruction : WorkerInstruction
	{
		private SceneBehavior behavior;
		private RuntimeSceneData sceneData;

		public SceneOPInstruction(SceneBehavior behavior, RuntimeSceneData sceneData)
		{
			this.behavior = behavior;
			this.sceneData = sceneData;
		}

		public SceneBehavior Behavior { get => behavior; }
		public RuntimeSceneData SceneData { get => sceneData;}
	}
}

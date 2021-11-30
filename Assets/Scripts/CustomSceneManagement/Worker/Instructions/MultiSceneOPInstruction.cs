using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core.Worker.Instructions
{
	class MultiSceneOPInstruction : WorkerInstruction
	{
		private List<SceneOPInstruction> sceneOPInstructions;

		public MultiSceneOPInstruction(List<SceneOPInstruction> sceneOPInstructions)
		{
			this.sceneOPInstructions = sceneOPInstructions;
		}

		public List<SceneOPInstruction> SceneOPInstructions { get => sceneOPInstructions;}
	}
}

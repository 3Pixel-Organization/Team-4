using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core.Worker.Instructions
{
	public class DelayOPInstruction : WorkerInstruction
	{
		private float timeDelay;
		public float TimeDelay { get => timeDelay; }

		public DelayOPInstruction(float timeDelay)
		{
			this.timeDelay = timeDelay;
		}
	}
}
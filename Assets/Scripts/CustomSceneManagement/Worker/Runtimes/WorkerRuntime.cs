using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core.Worker.Runtimes
{
	public class WorkerRuntime
	{
		protected Action callback;

		protected bool isDone = false;
		public bool IsDone => isDone;

		public virtual float Progress()
		{
			return 0;
		}

		public WorkerRuntime(Action callback)
		{
			this.callback = callback;
		}

		protected void Complete()
		{
			isDone = true;
			callback?.Invoke();
		}
	}
}
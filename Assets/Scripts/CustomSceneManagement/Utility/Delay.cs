using System;

using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CustomSceneManagement.Utility
{
	class Delay
	{
		private float delaylength, currentTime;
		private Action callback;

		private Thread thread;

		public Delay(float delay, Action callback)
		{
			this.callback = callback;
			delaylength = delay * 1000;
			thread = new Thread(new ThreadStart(GoDelay));
			thread.Start();
		}

		private void GoDelay()
		{
			Thread.Sleep(Mathf.RoundToInt(delaylength));
			callback?.Invoke();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSceneManagement.Core.Worker;

namespace CustomSceneManagement
{
	public class LoadingScreenUI : MonoBehaviour
	{
		[SerializeField]
		private TMPro.TextMeshProUGUI loadingText;

		private SceneWorker worker;

		public void Activate()
		{
			gameObject.SetActive(true);
			worker = FindObjectOfType<SceneWorker>();
		}

		public void DeActivate()
		{
			gameObject.SetActive(false);
			worker = null;
		}

		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			if(worker != null)
			{
				loadingText.SetText("Progress: " + worker.TotalProgress());
			}
		}
	}
}


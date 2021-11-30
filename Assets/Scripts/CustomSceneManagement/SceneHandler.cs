using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSceneManagement.Core;

namespace CustomSceneManagement
{
	public static class SceneHandler
	{
		public static void LoadCollection(SceneCollectionSO sceneCollection)
		{
			SceneDirector.LoadCollection(sceneCollection);
		}

		public static void LoadScene(SceneDataSO sceneDataSO)
		{
			SceneDirector.LoadScene(sceneDataSO);
		}

		public static Action FinishedLoading;
	}
}


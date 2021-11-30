using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomSceneManagement.Core
{
	public static class SceneWatcher
	{
		private static List<RuntimeSceneData> loadedScenes = new List<RuntimeSceneData>();

		[RuntimeInitializeOnLoadMethod]
		static void OnGameLoad()
		{
			loadedScenes.Add(new RuntimeSceneData(SceneManager.GetActiveScene()));
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;
			SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		}

		private static void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadMode)
		{
			if(loadMode == LoadSceneMode.Single)
			{
				loadedScenes.Clear();
			}
			loadedScenes.Add(new RuntimeSceneData(scene));
			//string loadedScenesString = "";
			//loadedScenes.ForEach(scene => { loadedScenesString += scene.SceneName + ", "; });
			//Debug.Log(loadedScenesString);
		}

		private static void SceneManager_sceneUnloaded(Scene scene)
		{
			loadedScenes.Remove(new RuntimeSceneData(scene));
			//string loadedScenesString = "";
			//loadedScenes.ForEach(scene => { loadedScenesString += scene.SceneName + ", "; });
			//Debug.Log(loadedScenesString);
		}

		public static List<RuntimeSceneData> GetRuntimeScenes()
		{
			return loadedScenes;
		}
	}
}

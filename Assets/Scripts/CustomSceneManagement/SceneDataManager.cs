using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement.Core
{
	public static class SceneDataManager
	{

		private static List<SceneDataSO> sceneSODataCache = new List<SceneDataSO>();

		[RuntimeInitializeOnLoadMethod]
		static void OnGameLoad()
		{
			ReloadCache();
		}

		public static void ReloadCache()
		{
			SceneDataSO[] sceneDatas = Resources.FindObjectsOfTypeAll<SceneDataSO>();
			sceneSODataCache = new List<SceneDataSO>(sceneDatas);
			//string loadedScenesString = "";
			//sceneSODataCache.ForEach(scene => { loadedScenesString += scene.SceneName + ", "; });
			//Debug.Log(loadedScenesString);
			//Debug.Log(sceneDatas.Length);
		}

		public static bool TryGetSceneDataSO(string sceneName, out SceneDataSO sceneDataSO)
		{
			if(sceneSODataCache.Count == 0)
			{
				ReloadCache();
			}

			foreach (SceneDataSO item in sceneSODataCache)
			{
				if(item.SceneName == sceneName)
				{
					sceneDataSO = item;
					return true;
				}
			}
			sceneDataSO = null;
			return false;
		}

		public static List<SceneDataSO> GetAllSystemScenes()
		{
			List<SceneDataSO> output = new List<SceneDataSO>();

			if (sceneSODataCache.Count == 0)
			{
				ReloadCache();
			}

			foreach (SceneDataSO item in sceneSODataCache)
			{
				if (item.Type == SceneType.Systems)
				{
					output.Add(item);
				}
			}
			return output;
		}

		public static List<SceneDataSO> GetAllUIScenes()
		{
			List<SceneDataSO> output = new List<SceneDataSO>();

			if (sceneSODataCache.Count == 0)
			{
				ReloadCache();
			}

			foreach (SceneDataSO item in sceneSODataCache)
			{
				if (item.Type == SceneType.UI)
				{
					output.Add(item);
				}
			}
			return output;
		}

		public static List<SceneDataSO> GetAll()
		{
			if (sceneSODataCache.Count == 0)
			{
				ReloadCache();
			}

			return sceneSODataCache;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSceneManagement
{
	[CreateAssetMenu(menuName = "Custom data/Scene collection")]
	public class SceneCollectionSO : ScriptableObject
	{
		public bool includeSystemScenes = true;
		public bool includeUIScenes = true;
		public LoadBehaviors loadBehaviors;

		public List<SceneDataSO> toLoad;

		public bool hasLoadingScreen;
		public SceneDataSO loadingScreen;
		public float loadingScreenStartStopDelay = 0.2f;

		[System.Serializable]
		public struct LoadBehaviors
		{
			public bool loadAdditive;
			public bool keepSystemScenes;
			public bool reloadDuplicates;
			public bool ignoreDuplicates;
		}
	}
}



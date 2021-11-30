using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CustomSceneManagement
{
	[CreateAssetMenu(menuName = "Custom data/Scene data")]
	public class SceneDataSO : ScriptableObject
	{
		[SerializeField]
		private string displayName;

		public string DisplayName {
			get 
			{
				string sceneStateText = "";
#if UNITY_EDITOR
				if(scene == null)
				{
					sceneStateText = " - Scene data not connected to scene: " + name;
				}
				else if(IsLoaded)
				{
					sceneStateText = " - Loaded";
				}
				else
				{
					sceneStateText = " - Not loaded";
				}
#endif
				if(displayName == "" || displayName == null)
				{
					return sceneName + sceneStateText;
				}
				else
				{
					return displayName + sceneStateText;
				}
				
			}
		}

#if UNITY_EDITOR
		[SerializeField]
		private SceneAsset scene;
#endif

		[SerializeField]
		private SceneType type;

		public SceneType Type { get { return type; } }

		[SerializeField]
		private bool manualUse = false;

		public bool ManualUse { get => manualUse; }

		[HideInInspector]
		[SerializeField]
		private string sceneName;

		public string SceneName { 
			get
			{
#if UNITY_EDITOR
				if (scene == null)
				{
					sceneName = "Empty";
					Debug.LogWarning(name + " needs to have a scene connected");
				}
				else
				{
					sceneName = scene.name;
				}
#endif
				return sceneName; 
			} 
		}

		private bool isLoaded;

		public bool IsLoaded
		{
			get
			{
				if(sceneName == null)
				{
					isLoaded = false;
					return isLoaded;
				}

#if UNITY_EDITOR
				isLoaded = EditorSceneManager.GetSceneByName(sceneName).isLoaded;
				return isLoaded;
#endif
#pragma warning disable CS0162 // Unreachable code detected: will be reachable if it is a build
				isLoaded = SceneManager.GetSceneByName(sceneName).isLoaded;
#pragma warning restore CS0162 // Unreachable code detected
				return isLoaded;
			}
		}

#if UNITY_EDITOR
		public void Setup(SceneAsset sceneAsset, SceneType sceneType)
		{
			scene = sceneAsset;
			type = sceneType;
			sceneName = sceneAsset.name;
		}
#endif

		private Scene activeScene;

		public Scene GetActiveScene()
		{
			return SceneManager.GetSceneByName(sceneName);
		}

		public void SetScene(Scene scene)
		{
			activeScene = scene;
		}

		private void OnValidate()
		{
#if UNITY_EDITOR
			if (scene != null)
			{
				sceneName = scene.name;
			}
#endif
		}

		public bool IsValid()
		{
#if UNITY_EDITOR
			if(scene == null)
			{
				return false;
			}
#endif
			if(sceneName == null)
			{
				return false;
			}
			return true;
		}
	}
}


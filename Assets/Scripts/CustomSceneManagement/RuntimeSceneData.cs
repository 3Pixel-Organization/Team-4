using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CustomSceneManagement.Core
{
	public class RuntimeSceneData
	{
		private string sceneName;

		public string SceneName { get => sceneName; }

		private SceneType type;

		public SceneType Type { get { return type; } }

		private Scene scene;

		public Scene GetScene { 
			get 
			{
				if (!scene.IsValid())
				{
					scene = SceneManager.GetSceneByName(sceneName);
				}
				return scene;
			} 
		}

		private bool isLoaded;

		public bool IsLoaded
		{
			get
			{
				if (sceneName == null)
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

		public RuntimeSceneData(SceneDataSO sceneDataSO)
		{
			AssignSceneDataSO(sceneDataSO);
			scene = SceneManager.GetSceneByName(sceneName);
		}

		public RuntimeSceneData(Scene scene)
		{
			if (SceneDataManager.TryGetSceneDataSO(scene.name, out SceneDataSO sceneDataSO))
			{
				AssignSceneDataSO(sceneDataSO);
			}
			else
			{
				AssignMissingSceneDataSO(scene);
			}
			AssignScene(scene);
		}

		private void AssignSceneDataSO(SceneDataSO sceneDataSO)
		{
			type = sceneDataSO.Type;
			sceneName = sceneDataSO.SceneName;
		}

		private void AssignMissingSceneDataSO(Scene scene)
		{
			type = SceneType.Unknown;
			sceneName = scene.name;
		}

		private void AssignScene(Scene scene)
		{
			this.scene = scene;
		}

		public override bool Equals(object obj)
		{
			if (obj is RuntimeSceneData otherObj)
			{
				return sceneName == otherObj.sceneName;
			}
			else
			{
				return false;
			}
		}

		public override string ToString()
		{
			return base.ToString();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator RuntimeSceneData(SceneDataSO sceneDataSO)
		{
			return new RuntimeSceneData(sceneDataSO);
		}

	}
}


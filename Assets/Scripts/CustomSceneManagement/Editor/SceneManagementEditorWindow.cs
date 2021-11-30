using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using CustomSceneManagement.Core;

namespace CustomSceneManagement
{
	public class SceneManagementEditorWindow : EditorWindow
	{
		[SerializeField]
		private Texture2D notLoadedImg;

		[SerializeField]
		private Texture2D loadedImg;

		bool includeSystemAndUIScenes = true, saveOnSceneLeave = true, toggleOrReplace, showSettings;

		private static List<Scene> missingSceneDataSOs = new List<Scene>();

		[MenuItem("Tools/Scene management/Scene management window")]
		public static void ShowWindow()
		{
			/*
			if (!EditorPrefs.GetBool("SceneManagementSystemIsSetup", false))
			{
				if(EditorUtility.DisplayDialog("Scene management system setup", "Do you want to setup the Scene management system to start using it?", "Yes", "No"))
				{
					SetupWizard.StartSetup();
				}
				else
				{
					return;
				}
			}
			*/

			GetWindow<SceneManagementEditorWindow>("Scene management");
		}

		public static void CreateSceneSOData(Scene scene, SceneType sceneType)
		{
			SceneDataSO asset = CreateInstance<SceneDataSO>();

			SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);

			string name = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/SceneData/" + scene.name + ".asset");

			asset.Setup(sceneAsset, sceneType);

			AssetDatabase.CreateAsset(asset, name);
			AssetDatabase.SaveAssets();
			SceneDataManager.ReloadCache();
		}

		private void Awake()
		{
			sceneTypesToCreate = new SceneType[1000];
			
		}

		private void OnDestroy()
		{
			
		}

		private void OnProjectChange()
		{
			SceneDataManager.ReloadCache();
		}

		SceneType[] sceneTypesToCreate;

		Vector2 scrollPosition;


		/// <summary>
		/// Takes care of all the UI
		/// </summary>
		private void OnGUI()
		{
			GUIStyle sectionTitleStyle = new GUIStyle
			{
				fontSize = 15,
				normal = new GUIStyleState
				{
					textColor = Color.white
				}
			};

			GUIStyle sceneStatusImgStyle = new GUIStyle
			{
				fixedWidth = 18,
				fixedHeight = 18,
				margin = new RectOffset
				{
					right = 0,
					left = 5,
					top = 1,
					bottom = 1
				}
			};

			scrollPosition = GUILayout.BeginScrollView(scrollPosition);

			GUILayout.Label("Content Scenes", sectionTitleStyle);
			SceneDataSO[] sceneDatas = Resources.FindObjectsOfTypeAll<SceneDataSO>();

			foreach (SceneDataSO item in sceneDatas)
			{
				if(item.Type == SceneType.Level)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Box(item.IsLoaded ? loadedImg : notLoadedImg, sceneStatusImgStyle);
					if (GUILayout.Button(new GUIContent(item.DisplayName, "Unloadeds and loads the level")) && item.IsValid())
					{
						if (saveOnSceneLeave)
						{
							EditorSceneManager.SaveOpenScenes();
						}
						else
						{
							if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
							{
								return;
							}
						}
						if (toggleOrReplace)
						{
							if (!item.IsLoaded)
							{
								EditorSceneManager.OpenScene("Assets/Scenes/" + item.SceneName + ".unity", OpenSceneMode.Additive);
							}
							else
							{
								EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneByName(item.SceneName));
							}
						}
						else
						{
							EditorSceneManager.OpenScene("Assets/Scenes/" + item.SceneName + ".unity", OpenSceneMode.Single);
							if (includeSystemAndUIScenes)
							{
								foreach (SceneDataSO item2 in sceneDatas)
								{
									if ((item2.Type == SceneType.Systems || item2.Type == SceneType.UI) && item2.IsValid())
									{
										EditorSceneManager.OpenScene("Assets/Scenes/" + item2.SceneName + ".unity", OpenSceneMode.Additive);
									}
								}
							}
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			includeSystemAndUIScenes = EditorGUILayout.Toggle("Include system and ui", includeSystemAndUIScenes);
			if (!includeSystemAndUIScenes)
			{
				toggleOrReplace = EditorGUILayout.Toggle("Toggle or replace", toggleOrReplace);
			}
			else
			{
				toggleOrReplace = false;
			}


			GUILayout.Space(10);
			GUILayout.Label("System Scenes", sectionTitleStyle);
			foreach (SceneDataSO item in sceneDatas)
			{
				if (item.Type == SceneType.Systems)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Box(item.IsLoaded ? loadedImg : notLoadedImg, sceneStatusImgStyle);
					if (GUILayout.Button(item.DisplayName) && item.IsValid())
					{
						if(!item.IsLoaded)
						{
							EditorSceneManager.OpenScene("Assets/Scenes/" + item.SceneName + ".unity", OpenSceneMode.Additive);
						}
						else
						{
							EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneByName(item.SceneName));
						}
					}
					GUILayout.EndHorizontal();
				}
			}

			GUILayout.Space(10);
			GUILayout.Label("UI Scenes", sectionTitleStyle);
			foreach (SceneDataSO item in sceneDatas)
			{
				if (item.Type == SceneType.UI)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Box(item.IsLoaded ? loadedImg : notLoadedImg, sceneStatusImgStyle);
					if (GUILayout.Button(item.DisplayName) && item.IsValid())
					{
						if (!item.IsLoaded)
						{
							EditorSceneManager.OpenScene("Assets/Scenes/" + item.SceneName + ".unity", OpenSceneMode.Additive);
						}
						else
						{
							EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneByName(item.SceneName));
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.Space(20);
			showSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showSettings, "General settings");
			if (showSettings)
			{
				EditorGUI.indentLevel++;
				saveOnSceneLeave = EditorGUILayout.Toggle("Auto save on load", saveOnSceneLeave);
				saveOnSceneLeave = false;
				GUILayout.Space(20);
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			List<EditorBuildSettingsScene> currentBuildScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
			for (int i = 0; i < EditorSceneManager.loadedSceneCount; i++)
			{
				Scene scene = EditorSceneManager.GetSceneAt(i);
				
				if (SceneDataManager.TryGetSceneDataSO(scene.name, out SceneDataSO sceneDataSO))
				{
					EditorBuildSettingsScene buildSettingsScene = new EditorBuildSettingsScene(scene.path, true);
					bool contains = false;
					for (int ci = 0; ci < currentBuildScenes.Count; ci++)
					{
						if(currentBuildScenes[ci].path == scene.path)
						{
							contains = true;
						}
					}
					if (!contains)
					{
						if (GUILayout.Button("Add to build: " + scene.name))
						{
							currentBuildScenes.Add(buildSettingsScene);
						}
					}
				}
				else //When failes to get SceneDataSO
				{
					GUILayout.Label("Create scene data for: " + scene.name);
					if(sceneTypesToCreate.Length <= i + 10)
					{
						SceneType[] increasedTypeAr = new SceneType[(sceneTypesToCreate.Length + 1) * 2];
						sceneTypesToCreate.CopyTo(increasedTypeAr, 0);
						sceneTypesToCreate = increasedTypeAr;
					}
					sceneTypesToCreate[i] = (SceneType)EditorGUILayout.EnumPopup(sceneTypesToCreate[i]);
					if (GUILayout.Button("Create"))
					{
						CreateSceneSOData(scene, sceneTypesToCreate[i]);
					}
				}
			}

			EditorBuildSettings.scenes = currentBuildScenes.ToArray();

			GUILayout.EndScrollView();

			/*
			
			
			Scene scene = SceneManager.GetActiveScene();
			sceneName = scene.name;

			

			if (GUILayout.Button("Create data"))
			{
				SceneDataSO asset = CreateInstance<SceneDataSO>();
				asset.SetData(sceneName, sceneType);
				AssetDatabase.CreateAsset(asset, "Assets/Resources/SceneData/" + sceneName + ".asset");
				AssetDatabase.SaveAssets();
			}
			*/
		}
	}
}


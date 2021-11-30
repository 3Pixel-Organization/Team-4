using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

using CustomSceneManagement.Operations;
using CustomSceneManagement.Core.Worker.Instructions;
using CustomSceneManagement.Core.Worker;

namespace CustomSceneManagement.Core
{
	public static class SceneDirector
	{
		private static SceneDataSO activeContentScene;

		private static List<SceneDataSO> currentlyLoadedScenes = new List<SceneDataSO>();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="scene">scene data</param>
		public static void LoadScene(SceneDataSO scene)
		{
			LocalLoadScene(scene, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="scene">scene data</param>
		/// <param name="unloadOld">Unloades old scene if true</param>
		public static void LoadScene(SceneDataSO scene, bool unloadOld)
		{
			LocalLoadScene(scene, unloadOld);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="scene">scene data</param>
		/// <param name="unloadOld">Unloades old scene if true</param>
		public static void LoadSceneAsync(SceneDataSO scene, bool unloadOld)
		{
			LocalLoadSceneAsync(scene, unloadOld);
		}

		public static void LocalLoadScene(SceneDataSO scene, bool unloadOld)
		{
			if (scene.Type == SceneType.Level)
			{
				if (unloadOld)
				{
					CheckForActiveContentScene();
					if (activeContentScene != null)
					{
						LocalUnloadScene(activeContentScene);
					}
				}
				SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
				Scene loadedScene = SceneManager.GetSceneByName(scene.SceneName);
				scene.SetScene(loadedScene);
				activeContentScene = scene;
			}
			else if(scene.Type == SceneType.Systems)
			{
				SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
			}
			else if(scene.Type == SceneType.UI)
			{
				SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
			}
			currentlyLoadedScenes.Add(scene);
		}

		public static void LocalLoadSceneAsync(SceneDataSO scene, bool unloadOld)
		{
			if (unloadOld)
			{
				CheckForActiveContentScene();
				if (activeContentScene != null)
				{
					LocalUnloadScene(activeContentScene);
				}
			}
			AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
			loadSceneOperation.completed += LoadSceneOperation_completed;
			currentlyLoadingScene = scene;
			activeContentScene = scene;
			currentlyLoadedScenes.Remove(scene);
		}

		public static Action FinishedLoading;

		private static SceneDataSO currentlyLoadingScene;

		private static void LoadSceneOperation_completed(AsyncOperation obj)
		{
			currentlyLoadingScene.SetScene(SceneManager.GetSceneByName(currentlyLoadingScene.name));
			currentlyLoadingScene = null;
			FinishedLoading.Invoke();
		}

		public static void UnloadScene(SceneDataSO scene)
		{
			LocalUnloadScene(scene);
		}

		public static void LocalUnloadScene(SceneDataSO scene)
		{
			SceneManager.UnloadSceneAsync(scene.GetActiveScene());
			currentlyLoadedScenes.Remove(scene);
			activeContentScene = null;
		}

		private static void CheckForActiveContentScene()
		{
			if (activeContentScene != null)
				return;

			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene scene = SceneManager.GetSceneAt(i);
				SceneDataSO sceneData = Resources.Load<SceneDataSO>("SceneData/" + scene.name);
				if(sceneData != null)
				{
					if(sceneData.Type == SceneType.Level)
					{
						sceneData.SetScene(scene);
						activeContentScene = sceneData;
					}
				}
			}
		}

		private static SceneCollectionSO c_SceneCollection;
		private static List<AsyncOperation> operations;
		private static GameObject sceneWorkerGameObj;

		public static void LoadCollection(SceneCollectionSO sceneCollection)
		{
			
			c_SceneCollection = sceneCollection;
			List<WorkerInstruction> workerInstructions = new List<WorkerInstruction>();

			List<SceneDataSO> scenesToLoad = new List<SceneDataSO>(sceneCollection.toLoad);

			List<RuntimeSceneData> handledScenes = new List<RuntimeSceneData>();
			List<RuntimeSceneData> currentScenes = SceneWatcher.GetRuntimeScenes();

			if (sceneCollection.hasLoadingScreen)
			{
				workerInstructions.Add(new LoadingScreenOPInstruction(sceneCollection.loadingScreen, LoadingScreenBehavior.Start));
				workerInstructions.Add(new DelayOPInstruction(sceneCollection.loadingScreenStartStopDelay));
			}

			if (sceneCollection.includeSystemScenes)
			{
				scenesToLoad.AddRange(SceneDataManager.GetAllSystemScenes());
			}
			if (sceneCollection.includeUIScenes)
			{
				scenesToLoad.AddRange(SceneDataManager.GetAllUIScenes());
			}

			List<SceneOPInstruction> sceneOPInstructions = new List<SceneOPInstruction>();

			foreach (SceneDataSO item in scenesToLoad)
			{
				RuntimeSceneData runtimeSceneData = new RuntimeSceneData(item);

				if (currentScenes.Contains(runtimeSceneData))
				{
					if (sceneCollection.loadBehaviors.ignoreDuplicates || !sceneCollection.loadBehaviors.reloadDuplicates)
					{
						handledScenes.Add(runtimeSceneData);
						continue;
					}
					else if (c_SceneCollection.loadBehaviors.reloadDuplicates)
					{
						sceneOPInstructions.Add(new SceneOPInstruction(SceneBehavior.Reload, runtimeSceneData));
						handledScenes.Add(runtimeSceneData);
						continue;
					}
				}
				else
				{
					sceneOPInstructions.Add(new SceneOPInstruction(SceneBehavior.Load, runtimeSceneData));
					handledScenes.Add(runtimeSceneData);
					continue;
				}
			}

			foreach (RuntimeSceneData runtimeSceneData in currentScenes)
			{
				switch (runtimeSceneData.Type)
				{
					case SceneType.LoadingScreen:
					case SceneType.Ignore:
						continue;
				}

				if (!handledScenes.Contains(runtimeSceneData))
				{
					if (!sceneCollection.loadBehaviors.loadAdditive)
					{
						sceneOPInstructions.Add(new SceneOPInstruction(SceneBehavior.Unload, runtimeSceneData));
					}
				}
			}

			workerInstructions.Add(new MultiSceneOPInstruction(sceneOPInstructions));


			if (sceneCollection.hasLoadingScreen)
			{
				workerInstructions.Add(new DelayOPInstruction(sceneCollection.loadingScreenStartStopDelay));
				workerInstructions.Add(new LoadingScreenOPInstruction(sceneCollection.loadingScreen, LoadingScreenBehavior.Stop));
			}

			sceneWorkerGameObj = new GameObject("SceneWorker");

			SceneWorker sceneWorker = sceneWorkerGameObj.AddComponent<SceneWorker>();

			sceneWorker.AssignInstructions(workerInstructions);
			sceneWorker.StartWork();
		}

		public static void WorkerComplete()
		{
			//Debug.Log("Scene worker done");
			SceneHandler.FinishedLoading?.Invoke();
		}

		private static void ReloadAndRemove()
		{
			List<SceneOperationParms> sceneInstructions = new List<SceneOperationParms>();
			List<SceneDataSO> handledScenes = new List<SceneDataSO>();
			List<SceneDataSO> removeScenes = new List<SceneDataSO>();
			foreach (SceneDataSO sceneLoaded in currentlyLoadedScenes)
			{
				if (c_SceneCollection.toLoad.Contains(sceneLoaded))
				{
					if (c_SceneCollection.loadBehaviors.ignoreDuplicates || !c_SceneCollection.loadBehaviors.reloadDuplicates || sceneLoaded.Type == SceneType.Systems)
					{
						handledScenes.Add(sceneLoaded);
						continue;
					}
					else if (c_SceneCollection.loadBehaviors.reloadDuplicates)
					{
						sceneInstructions.Add(new SceneOperationParms()
						{
							scene = sceneLoaded,
							operationBehavior = SceneOperationBehavior.Reload
						});
						handledScenes.Add(sceneLoaded);
						continue;
					}
				}
				else if(!c_SceneCollection.loadBehaviors.loadAdditive)
				{
					sceneInstructions.Add(new SceneOperationParms()
					{
						scene = sceneLoaded,
						operationBehavior = SceneOperationBehavior.Unload
					});
					handledScenes.Add(sceneLoaded);
					removeScenes.Add(sceneLoaded);
					continue;
				}
			}

			foreach (SceneDataSO item in removeScenes)
			{
				currentlyLoadedScenes.Remove(item);
			}

			foreach (SceneDataSO sceneToLoad in c_SceneCollection.toLoad)
			{
				if (!handledScenes.Contains(sceneToLoad))
				{
					sceneInstructions.Add(new SceneOperationParms()
					{
						scene = sceneToLoad,
						operationBehavior = SceneOperationBehavior.Load
					});
					handledScenes.Add(sceneToLoad);
					currentlyLoadedScenes.Add(sceneToLoad);
				}
			}
			MultiSceneOperation multiSceneOperation = new MultiSceneOperation(sceneInstructions, NextStep);
			multiSceneOperation.Start();
		}

		public static void NextStep()
		{
			
			//new SceneAsyncOperation(currentSceneCollection.loadingScreen, FinishedLoading, SceneOperationBehavior.Unload);
			
		}

		private static void CallBacked()
		{
			StopLoadingScreen(c_SceneCollection.loadingScreen, FinishedLoading);
		}

		static SceneDataSO c_loadingScene;
		static Action c_loadSceneReturn;

		private static void StartLoadingScreen(SceneDataSO scene, Action onComplete)
		{
			c_loadingScene = scene;
			c_loadSceneReturn = onComplete;
			if (scene.IsLoaded)
			{
				ActivateLoadingScene();
			}
			else
			{
				new SceneAsyncOperation(scene, ActivateLoadingScene, SceneOperationBehavior.Load);
			}

			static void ActivateLoadingScene()
			{
				bool returned = false;
				if (c_loadingScene.IsLoaded)
				{
					GameObject[] gameObjects = c_loadingScene.GetActiveScene().GetRootGameObjects();
					for (int i = 0; i < gameObjects.Length; i++)
					{
						if (gameObjects[i].TryGetComponent(out ILoadingScreenUI loadingScreenUI))
						{
							loadingScreenUI.Activate();
							if (!returned)
							{
								//loadingScreenUI.SubToComplete(c_loadSceneReturn);
								returned = true;
							}
								
						}
					}
					if (!returned)
					{
						c_loadSceneReturn?.Invoke();
						returned = true;
					}
				}
			}
		}

		private static void StopLoadingScreen(SceneDataSO scene, Action onComplete)
		{
			if (scene.IsLoaded)
			{
				GameObject[] gameObjects = scene.GetActiveScene().GetRootGameObjects();
				for (int i = 0; i < gameObjects.Length; i++)
				{
					if (gameObjects[i].TryGetComponent(out ILoadingScreenUI loadingScreenUI))
					{
						loadingScreenUI.DeActivate();
					}
				}
			}
			onComplete?.Invoke();
		}
	}	
}



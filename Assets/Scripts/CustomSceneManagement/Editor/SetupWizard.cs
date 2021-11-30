using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CustomSceneManagement
{
	public class SetupWizard
	{
		//[MenuItem("Tools/Scene management/Setup wizard", priority = 1)]
		public static void StartSetup()
		{
			/*
			if(EditorPrefs.GetBool("SceneManagementSystemIsSetup", false))
			{
				if(!EditorUtility.DisplayDialog("Redo scene system setup", "Do you want to redo scene management system setup", "Yes", "Cancel"))
				{
					return;
				}
			}
			*/

			if (!SetupFileSystem())
			{
				Debug.LogError("Failed to setup file structure, please try again with setup");
			}

			//EditorPrefs.SetBool("SceneManagementSystemIsSetup", true);
		}

		static bool SetupFileSystem()
		{
			if (!AssetDatabase.IsValidFolder("Assets/Resources"))
			{
				string ResGUID = AssetDatabase.CreateFolder("Assets", "Resources");
				if (ResGUID == null)
					return false;
			}
			if (!AssetDatabase.IsValidFolder("Assets/Resources/SceneData"))
			{
				string SceDataGUID = AssetDatabase.CreateFolder("Assets/Resources", "SceneData");
				if (SceDataGUID == null)
					return false;
			}
			
			return true;
		}
	}
}


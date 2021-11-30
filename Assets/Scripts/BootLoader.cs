using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSceneManagement;

public class BootLoader : MonoBehaviour
{
	[SerializeField]
	private SceneCollectionSO sceneCollection;

	// Start is called before the first frame update
	void Start()
	{
		//foreach (SceneDataSO item in scenesToLoad)
		//{
		//	SceneDirector.LoadScene(item);
		//}

		SceneHandler.LoadCollection(sceneCollection);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}

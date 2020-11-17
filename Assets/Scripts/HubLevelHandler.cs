using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubLevelHandler : MonoBehaviour
{
	GameResource gameResource;
	// Start is called before the first frame update
	private void Awake()
	{
		gameResource = Resources.Load<GameResource>("Game");
	}

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void StartLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public void QuitMenu()
	{
		GetComponentInParent<Canvas>().gameObject.SetActive(false);
	}
}

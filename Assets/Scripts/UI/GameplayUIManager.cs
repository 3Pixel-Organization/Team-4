using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
	public static GameplayUIManager current;

	[SerializeField] private GameObject gameplayUI;
	[SerializeField] private GameObject inventoryUI;

	private void Awake()
	{
		if(current != null)
		{
			Debug.LogWarning("There is more then on GameplayUIHandler in the scene");
		}
		current = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OpenInventory()
	{
		GameManager.current.PauseGame();
		inventoryUI.SetActive(true);
		gameplayUI.SetActive(false);
	}

	public void CloseInventory()
	{
		GameManager.current.UnPauseGame();
		inventoryUI.SetActive(false);
		gameplayUI.SetActive(true);
	}
}

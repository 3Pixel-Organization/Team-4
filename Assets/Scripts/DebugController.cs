using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DebugController : MonoBehaviour
{
	bool showConsole;
	bool showHelp;
	string input;

	public static DebugCommand WIN;
	public static DebugCommand INV_SAVE;
	public static DebugCommand INV_LOAD;
	public static DebugCommand CONSOLE_TEST;
	public static DebugCommand HELP;
	public static DebugCommand PLAYER_SAVE;
	public static DebugCommand PLAYER_LOAD;
	public static DebugCommand<int> PLAYER_SET_LVL;
	public static DebugCommand<int> PLAYER_SET_EXP;
	public static DebugCommand<int> PLAYER_ADD_EXP;

	public List<object> commandList;

	private void Awake()
	{
		WIN = new DebugCommand("win", "Triggers win function", "win", () =>
		{
			GameManager.current.Victory();
		});

		INV_SAVE = new DebugCommand("inv_save", "Saves current inventory", "inv_save", () =>
		{
			Inventory.Save();
		});

		INV_LOAD = new DebugCommand("inv_load", "Loads currently saved inventory", "inv_load", () =>
		{
			Inventory.Load();
		});

		CONSOLE_TEST = new DebugCommand("console_test", "tests that the console is working", "console_test", () =>
		{
			Debug.Log("Console test");
		});

		HELP = new DebugCommand("help", "Gives help", "help", () =>
		{
			showHelp = true;
		});

		PLAYER_LOAD = new DebugCommand("player_load", "Loads player data", "player_load", () =>
		{
			Player.Load();
		});

		PLAYER_SAVE = new DebugCommand("player_save", "Saves player data", "player_save", () =>
		{
			Player.Save();
		});

		PLAYER_SET_LVL = new DebugCommand<int>("player_set_lvl", "Sets the playerdata level", "player_set_lvl <level>", (lvl) =>
		{
			Player.levelSystem.SetLvl(lvl);
		});

		PLAYER_SET_EXP = new DebugCommand<int>("player_set_exp", "Sets the playerdata expPoints", "player_set_exp <expPoints>", (expPoints) =>
		{
			Player.levelSystem.SetExp(expPoints);
		});

		PLAYER_ADD_EXP = new DebugCommand<int>("player_add_exp", "Adds to the playerdata expPoints", "player_add_exp <expPoints>", (expPoints) =>
		{
			Player.levelSystem.GiveExp(expPoints);
		});

		commandList = new List<object>
		{
			WIN,
			INV_SAVE,
			INV_LOAD,
			CONSOLE_TEST,
			PLAYER_LOAD,
			PLAYER_SAVE,
			PLAYER_SET_LVL,
			PLAYER_SET_EXP,
			PLAYER_ADD_EXP,
			HELP
		};

	}

	Vector3 oldPos;
	float currentMoveSpeed;
	bool isMoving;

	private void Update()
	{
		isMoving = false;
		Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
		if (oldPos == null)
		{
			oldPos = currentPos;
		}
		Vector3 posDelta = currentPos - oldPos;
		if(posDelta.magnitude > 0)
		{
			currentMoveSpeed = posDelta.magnitude * Time.deltaTime;
			isMoving = true;
		}
	}

	public void OnToggleDebug(InputValue value)
	{
		showConsole = !showConsole;
	}
	public void OnReturn(InputValue value)
	{
		if (showConsole)
		{
			HandleInput();
			input = "";
		}
	}
	public void OnAutoComplete(InputValue value)
	{

	}

	Vector2 scroll;

	private void OnGUI()
	{
		if(!showConsole) { return; }

		float y = 0;

		if (showHelp)
		{
			GUI.Box(new Rect(0, y, Screen.width, 100), "");

			Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

			scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

			for (int i = 0; i < commandList.Count; i++)
			{
				DebugCommandBase command = commandList[i] as DebugCommandBase;

				string label = $"{command.CommandFormat} - {command.CommandDescription}";

				Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
				GUI.Label(labelRect, label);
			}
			GUI.EndScrollView();
			y += 100;
		}

		GUI.Box(new Rect(0, y, Screen.width, 30), "");
		GUI.backgroundColor = new Color(0, 0, 0, 0);
		input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
	}

	private void HandleInput()
	{
		string[] properties = input.Split(' ');
		for (int i = 0; i < commandList.Count; i++)
		{
			DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

			if (input.Contains(commandBase.CommandId))
			{
				if (commandList[i] as DebugCommand != null)
				{
					(commandList[i] as DebugCommand).Invoke();
				}
				else if(commandList[i] as DebugCommand<int> != null)
				{
					(commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
				}
			}
		}
	}
}

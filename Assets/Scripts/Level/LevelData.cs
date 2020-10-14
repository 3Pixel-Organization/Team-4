using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelData : ScriptableObject
{
	public string name;
	public int sceneNumb;
	public int difficultyLevel;
	public LootTable levelLootTable;
}

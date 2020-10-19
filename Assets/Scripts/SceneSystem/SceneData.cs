using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneData")]
public class SceneData : ScriptableObject
{
	public string name;
	public int sceneNumb;
	public int difficultyLevel;
	public LootTable levelLootTable;
}

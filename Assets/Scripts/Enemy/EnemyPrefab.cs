using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyPrefab : ScriptableObject
{
	public GameObject prefab;
	public int reletiveLevel;
	public float healthMultiplier;
	public EnemyTier enemyTier;
}

public enum EnemyTier
{
	Easy,
	Normal,
	Hard,
	Harder,
	Boss
}

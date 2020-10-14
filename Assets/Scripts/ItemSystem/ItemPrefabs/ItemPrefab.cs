using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/Item")]
public class ItemPrefab : ScriptableObject
{
	public GameObject model;
	public Sprite sprite;
}

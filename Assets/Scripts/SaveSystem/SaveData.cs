using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
class SaveData
{
	private static SaveData _current;
	public static SaveData Current
	{
		get
		{
			if (_current == null)
			{
				_current = new SaveData();
			}
			return _current;
		}
		set
		{
			_current = value;
		}
	}

	public List<ItemData> items = new List<ItemData>();
	public PlayerSaveData player;

}


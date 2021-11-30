using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
	public class GameEvents
	{
		public static GameEvents current;

		public PlayerEvents player;
		public UIEvents ui;
		public SpawnEvents spawn;
		public LevelEvents level;
		public EnemyEvents enemy;
		public CombatEvents combat;

		[RuntimeInitializeOnLoadMethod()]
		private static void Init()
		{
			current = new GameEvents();
		}

		public GameEvents()
		{
			player = new PlayerEvents();
			ui = new UIEvents();
			spawn = new SpawnEvents();
			level = new LevelEvents();
			enemy = new EnemyEvents();
			combat = new CombatEvents();
		}
	}
}

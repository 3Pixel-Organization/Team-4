using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;

public class BoltAttackCreator : MonoBehaviour
{
	public static Attack GetAttack(float damage, Attack.AttackType type)
	{
		return new Attack(damage, type);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using HealthV2;

[UnitTitle("Create attack")]
[UnitCategory("Utility")]
public class AttackCreator : Unit
{
	[DoNotSerialize]
	public ValueInput damageIn { get; private set; }

	[DoNotSerialize]
	public ValueInput attackTypeIn { get; private set; }

	[DoNotSerialize]
	public ValueOutput attackOut { get; private set; }
	protected override void Definition()
	{
		damageIn = ValueInput("Damage", 1);
		attackTypeIn = ValueInput("Type", Attack.AttackType.Light);

		attackOut = ValueOutput("Attack", GetAttack);
	}

	public Attack GetAttack(Flow flow)
	{
		float damage = flow.GetValue<float>(damageIn);
		Attack.AttackType type = flow.GetValue<Attack.AttackType>(attackTypeIn);

		return new Attack(damage, type);
	}
}

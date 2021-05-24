using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using HealthV2;

[UnitTitle("Guard")]
[UnitCategory("Enemy/State")]
public class EnemyGuard : Unit
{
	[DoNotSerialize]
	public ControlInput input { get; private set; }

	[DoNotSerialize]
	public ControlOutput finished { get; private set; }

	[DoNotSerialize]
	public ControlOutput broken { get; private set; }

	[DoNotSerialize]
	[NullMeansSelf]
	[PortLabelHidden]
	public ValueInput selfIn { get; private set; }

	[DoNotSerialize]
	public ValueInput timeIn { get; private set; }

	[DoNotSerialize]
	public ValueInput breakThreshold { get; private set; }

	public GraphReference graphReference { get; private set; }

	private Attack breakThresholdValue;
	private Enemy enemy;
	private Animator animator;
	private Flow currentFlow;

	protected override void Definition()
	{
		input = ControlInputCoroutine("Start", Enter);
		finished = ControlOutput("Finished");
		broken = ControlOutput("Broken");

		selfIn = ValueInput<GameObject>("Self", null).NullMeansSelf();
		timeIn = ValueInput<float>("Duration", 1);
		breakThreshold = ValueInput("Break threshold", new Attack(10, Attack.AttackType.Heavy));

		Succession(input, finished);
		Succession(input, broken);

		Requirement(timeIn, input);
		Requirement(breakThreshold, input);
	}

	public IEnumerator Enter(Flow flow)
	{
		currentFlow = flow;

		GameObject self = flow.GetValue<GameObject>(selfIn);

		float time = flow.GetValue<float>(timeIn);

		breakThresholdValue = flow.GetValue<Attack>(breakThreshold);

		graphReference = flow.stack.AsReference();

		enemy = self.GetComponent<Enemy>();
		animator = self.GetComponent<Animator>();

		if (!enemy.IsAlive)
		{
			currentFlow.StopCoroutine(true);
		}

		animator.SetBool("Blocking", true);

		enemy.Attacked += TryBreak;
		enemy.ReponseToAttack += TryAttack;

		enemy.enemyState = Enemy.EnemyState.Guard;

		yield return new WaitForSeconds(time);
		
		animator.SetBool("Blocking", false);
		enemy.enemyState = Enemy.EnemyState.Normal;
		enemy.Attacked -= TryBreak;
		enemy.ReponseToAttack -= TryAttack;
		yield return finished;
	}

	private void TryBreak(Attack attack)
	{
		if(attack >= breakThresholdValue)
		{
			animator.SetBool("Blocking", false);
			enemy.enemyState = Enemy.EnemyState.Vulnerable;
			enemy.Attacked -= TryBreak;
			enemy.ReponseToAttack -= TryAttack;
			Flow.New(graphReference).StartCoroutine(broken);
			currentFlow.StopCoroutine(true);
		}
	}

	private AttackResponse TryAttack(Attack attack)
	{
		if (attack >= breakThresholdValue)
		{
			animator.SetBool("Blocking", false);
			enemy.enemyState = Enemy.EnemyState.Vulnerable;
			enemy.Attacked -= TryBreak;
			enemy.ReponseToAttack -= TryAttack;
			Flow.New(graphReference).StartCoroutine(broken);
			currentFlow.StopCoroutine(true);
			return new AttackResponse(attack);
		}
		return AttackResponse.Blocked;
	}

}

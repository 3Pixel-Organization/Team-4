using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using HealthV2;

[UnitTitle("Attack")]
[UnitCategory("Enemy/State")]
public class EnemyAttack : Unit
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

	public GraphReference graphReference { get; private set; }

	private Enemy enemy;
	private Animator animator;
	private Flow currentFlow;

	private bool vulnerable = false;

	protected override void Definition()
	{
		input = ControlInputCoroutine("Start", Enter);
		finished = ControlOutput("Finished");
		broken = ControlOutput("Broken");
		
		selfIn = ValueInput<GameObject>("Self", null).NullMeansSelf();
		timeIn = ValueInput<float>("Duration", 1);

		Succession(input, finished);
		Succession(input, broken);

		Requirement(timeIn, input);
	}

	public IEnumerator Enter(Flow flow)
	{
		currentFlow = flow;

		GameObject self = flow.GetValue<GameObject>(selfIn);

		float time = flow.GetValue<float>(timeIn);

		graphReference = flow.stack.AsReference();

		enemy = self.GetComponent<Enemy>();
		animator = self.GetComponent<Animator>();

		if (!enemy.IsAlive)
		{
			currentFlow.StopCoroutine(true);
		}
		enemy.ReponseToAttack += TryCounter;


		//Attack start
		animator.SetBool("Attacking", true);
		enemy.enemyState = Enemy.EnemyState.Vulnerable;
		vulnerable = true;
		Player.Instance.Warn(new AttackWarning(self, AttackWarning.WarningType.StartAttack));

		yield return new WaitForSeconds(time);

		animator.SetBool("Attacking", false);
		enemy.enemyState = Enemy.EnemyState.Normal;
		vulnerable = false;
		enemy.ReponseToAttack -= TryCounter;
		yield return finished;
	}

	private AttackResponse TryCounter(Attack attack)
	{
		if (vulnerable)
		{
			if(attack.Type == Attack.AttackType.Heavy)
			{
				return new AttackResponse(attack.Damage, AttackResponse.HitResult.Heavy);
			}
			else
			{
				return AttackResponse.Blocked;
			}
		}
		else
		{
			return AttackResponse.Blocked;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using HealthV2;

[UnitTitle("Vulnerable")]
[UnitCategory("Enemy/State")]
public class EnemyVulnerable : Unit
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
		if (!enemy.IsAlive)
		{
			currentFlow.StopCoroutine(true);
		}
		animator = self.GetComponent<Animator>();

		//animator.SetBool("Blocking", true);

		enemy.ReponseToAttack += Attacked;

		enemy.enemyState = Enemy.EnemyState.Guard;

		yield return new WaitForSeconds(time);

		//animator.SetBool("Blocking", false);
		enemy.enemyState = Enemy.EnemyState.Normal;
		enemy.ReponseToAttack -= Attacked;
		yield return finished;
	}

	private AttackResponse Attacked(Attack attack)
	{
		enemy.enemyState = Enemy.EnemyState.Normal;
		enemy.ReponseToAttack -= Attacked;
		Flow.New(graphReference).StartCoroutine(broken);
		currentFlow.StopCoroutine(true);
		return new AttackResponse(attack);
	}
}

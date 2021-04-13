using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

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

	protected override void Definition()
	{
		input = ControlInputCoroutine("Start", Enter);
		finished = ControlOutput("Finished");
		broken = ControlOutput("Broken");

		selfIn = ValueInput<GameObject>("Self", null).NullMeansSelf();
		timeIn = ValueInput<float>("Duration", 0);
	}

	public IEnumerator Enter(Flow flow)
	{
		GameObject self = flow.GetValue<GameObject>(selfIn);
		float time = flow.GetValue<float>(timeIn);
		self.GetComponent<Enemy>().enemyState = Enemy.EnemyState.Guard;
		yield return broken;
		yield return new WaitForSeconds(time);
		self.GetComponent<Enemy>().enemyState = Enemy.EnemyState.Normal;
		yield return finished;
	}

}

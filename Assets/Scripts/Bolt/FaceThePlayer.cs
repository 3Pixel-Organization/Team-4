using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

[UnitTitle("Face the player")]
[UnitCategory("Utility/Transform")]
public class FaceThePlayer : Unit
{
	[DoNotSerialize]
	public ControlInput input { get; private set; }

	[DoNotSerialize]
	public ControlOutput output { get; private set; }

	[DoNotSerialize]
	public ValueInput selfTransformIn { get; private set; }

	protected override void Definition()
	{
		input = ControlInput("In", Enter);
		output = ControlOutput("Out");
		
		selfTransformIn = ValueInput<Transform>("Self");

		Requirement(selfTransformIn, input);
		Succession(input, output);
	}
	
	public ControlOutput Enter(Flow flow)
	{
		Transform selfTransform = flow.GetValue<Transform>(selfTransformIn);
		
		Quaternion quaternion = Quaternion.LookRotation(Player.Instance.transform.position - selfTransform.position);
		quaternion.x = selfTransform.rotation.x;
		quaternion.z = selfTransform.rotation.z;
		selfTransform.rotation = quaternion;
		return output;
	}
}

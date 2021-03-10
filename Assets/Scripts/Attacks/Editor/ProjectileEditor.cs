using UnityEditor;
using UnityEngine;
using Attacks;
using Attacks.Movment;

[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		//base.OnInspectorGUI();
		Projectile myProjectile = (Projectile)target;

		myProjectile.attackProps.DamageAmount = EditorGUILayout.FloatField("Damage", myProjectile.attackProps.DamageAmount);
		/*
		if (GUILayout.Button("Add"))
		{
			myProjectile.movment.movmentPatterns.Add(new Forward());
		}
		*/
	}

	private void OnSceneGUI()
	{
		Projectile projectile = target as Projectile;

		//Transform transform = projectile.transform;
		//projectile.movment.basicProps.position = transform.InverseTransformPoint(
		//	Handles.PositionHandle(
		//		transform.TransformPoint(projectile.movment.basicProps.position),
		//		transform.rotation));
	}
}

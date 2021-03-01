using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
	public float attackReach = 2f;

	private CharacterController characterController;

	[SerializeField] private Attack attack;

	// Start is called before the first frame update
	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Attack();
		}
	}

	public void Attack()
	{
		LayerMask layerMask = LayerMask.GetMask("Enemy");
		bool hitEnemy = false;
		RaycastHit hitInfo = new RaycastHit();
		float angleWidth = 90;
		for (float i = -angleWidth; i <= angleWidth; i += 10)
		{
			Ray ray = new Ray(transform.position + Vector3.up * 2, Quaternion.AngleAxis(i, Vector3.up) * transform.rotation * Vector3.forward);
			if(Physics.Raycast(ray, out hitInfo, attackReach, layerMask))
			{
				hitEnemy = true;
				break;
			}
		}
		if (hitEnemy)
		{
			characterController.Move(-(transform.position - hitInfo.collider.gameObject.transform.position) / 2f);
			transform.LookAt(hitInfo.collider.gameObject.transform);
			StartCoroutine(AttackDur());
		}
		else
		{
			return;
		}
	}

	private IEnumerator AttackDur()
	{
		attack.IsAttacking = true;
		yield return new WaitForSeconds(0.5f);
		attack.IsAttacking = false;
	}

	private void OnDrawGizmos()
	{
		LayerMask layerMask = LayerMask.GetMask("Enemy");
		float angleWidth = 90;
		for (float i = -angleWidth; i <= angleWidth; i += 10)
		{
			Ray ray = new Ray(transform.position + Vector3.up * 2, Quaternion.AngleAxis(i, Vector3.up) * transform.rotation * Vector3.forward);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, attackReach, layerMask))
			{
				Gizmos.DrawLine(transform.position + Vector3.up * 2, hitInfo.point);
			}
		}
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class PlayerCombatController : MonoBehaviour
{
	private CharacterController characterController;
	private PlayerController playerController;

	[SerializeField] private Attack attack;
	[SerializeField] private LayerMask layerMask;

	[System.Serializable]
	struct RayProps
	{
		public bool drawRays, drawHit;
		public Vector2 checkAngles;
		public float steps;
		public float checkDistance;
		public Vector3 offset;
	}

	enum HitState
	{
		None = 0,
		Enemy = 1,
		Shield = 2,
		Attack = 4,
	}

	enum PlayerCombatState
	{
		None,
		Attacking,
		Stagered,
	}

	private HitState hitState;
	private PlayerCombatState combatState;

	[SerializeField] private RayProps rayProps;

	// Start is called before the first frame update
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		playerController = GetComponent<PlayerController>();
		hitState = HitState.None;
		combatState = PlayerCombatState.None;
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
		if (combatState != PlayerCombatState.None) return;


		hitState = HitState.None;
		//layerMask = LayerMask.GetMask("Enemy");
		RaycastHit hitInfo = new RaycastHit();
		RaycastHit enemyHitRay = new RaycastHit();
		Vector2 stepSize = new Vector2(2 * rayProps.checkAngles.x / rayProps.steps, 2 * rayProps.checkAngles.y / rayProps.steps);
		for (float x = -rayProps.checkAngles.x; x <= rayProps.checkAngles.x; x += stepSize.x)
		{
			for (float y = -rayProps.checkAngles.y; y <= rayProps.checkAngles.y; y += stepSize.y)
			{
				Ray ray = new Ray(transform.position + rayProps.offset, transform.rotation * Quaternion.AngleAxis(y, Vector3.forward) * Quaternion.AngleAxis(x, Vector3.up) * Vector3.forward);
				if (Physics.Raycast(ray, out hitInfo, rayProps.checkDistance, layerMask))
				{
					if (hitInfo.collider.CompareTag("Shield"))
					{
						hitState |= HitState.Shield;
					}
					if(hitInfo.collider.CompareTag("Enemy"))
					{
						hitState |= HitState.Enemy;
						enemyHitRay = hitInfo;
					}
				}
			}
		}
		if((hitState & HitState.Enemy) == HitState.Enemy)
		{
			//Hit enemy
			characterController.Move(-(transform.position - enemyHitRay.collider.gameObject.transform.position) / 2f);
			transform.LookAt(enemyHitRay.collider.gameObject.transform);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			enemyHitRay.collider.gameObject.GetComponent<HealthController>().Damage(100000);
			StartCoroutine(AttackDur());
		}
		if(hitState == HitState.Shield)
		{
			//Stager player
			StartCoroutine(StagerPlayer(0.3f));
		}
	}

	private IEnumerator AttackDur()
	{
		combatState = PlayerCombatState.Attacking;
		playerController.DeactivateMovment();
		//attack.IsAttacking = true;
		yield return new WaitForSeconds(0.5f);
		//attack.IsAttacking = false;
		playerController.ActivateMovment();
		combatState = PlayerCombatState.None;
	}

	private IEnumerator StagerPlayer(float stagerTime)
	{
		combatState = PlayerCombatState.Stagered;
		playerController.DeactivateMovment();
		yield return new WaitForSeconds(stagerTime);
		playerController.ActivateMovment();
		combatState = PlayerCombatState.None;
	}

	private void OnDrawGizmos()
	{
		if (rayProps.drawRays || rayProps.drawHit)
		{
			Vector2 stepSize = new Vector2(2 * rayProps.checkAngles.x / rayProps.steps, 2 * rayProps.checkAngles.y / rayProps.steps);
			for (float x = -rayProps.checkAngles.x; x <= rayProps.checkAngles.x; x += stepSize.x)
			{
				for (float y = -rayProps.checkAngles.y; y <= rayProps.checkAngles.y; y += stepSize.y)
				{
					if (rayProps.drawHit)
					{
						Vector3 dir = Vector3.forward;
						dir = transform.rotation * Quaternion.AngleAxis(y, Vector3.right) * Quaternion.AngleAxis(x, Vector3.up) * dir;

						Ray ray = new Ray(transform.position + transform.rotation * rayProps.offset, dir);
						if (Physics.Raycast(ray, out RaycastHit hitInfo, rayProps.checkDistance, layerMask))
						{
							Gizmos.color = new Color((x + rayProps.checkAngles.x) / (rayProps.checkAngles.x * 2), 1, (y + rayProps.checkAngles.y) / (rayProps.checkAngles.y * 2), 0.2f);
							Gizmos.DrawLine(transform.position + transform.rotation * rayProps.offset, hitInfo.point);
						}
					}
					if (rayProps.drawRays)
					{
						Gizmos.color = new Color((x + rayProps.checkAngles.x) / (rayProps.checkAngles.x * 2), 0, (y + rayProps.checkAngles.y) / (rayProps.checkAngles.y * 2), 0.3f);
						
						Vector3 dir = Vector3.forward * rayProps.checkDistance;
						dir = transform.rotation * Quaternion.AngleAxis(y, Vector3.right) * Quaternion.AngleAxis(x, Vector3.up) * dir;
						dir += transform.position + rayProps.offset;
						Gizmos.DrawLine(transform.position + transform.rotation * rayProps.offset, dir);

						//Gizmos.DrawLine(transform.position + transform.rotation * rayProps.offset, Quaternion.AngleAxis(y, Vector3.forward) * Quaternion.AngleAxis(x, Vector3.up) * transform.rotation * Vector3.forward * rayProps.checkDistance + transform.position + rayProps.offset);
					}
				}
			}
		}
		
	}
}

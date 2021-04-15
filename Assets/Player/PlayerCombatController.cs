using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;
using EventSystem;

/// <summary>
/// MonoBehaviour that takes care of the player combat
/// </summary>
/// <creator>lolman</creator>
public class PlayerCombatController : MonoBehaviour
{
	private CharacterController characterController;
	private PlayerController playerController;

	//[SerializeField] private Attack attack;
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
	[System.Serializable]
	struct CombatProps
	{
		public float attackDuration;
		public StageredProps stageredProps;

		[System.Serializable]
		public struct StageredProps
		{
			public float shieldHit;
			public float smallHit;
			public float mediumHit;
			public float largeHit;
		}
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
	[SerializeField] private CombatProps combatProps;

	// Start is called before the first frame update
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		playerController = GetComponent<PlayerController>();
		Player.Instance.TakeDamage += DamagedStager;
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

	/// <summary>
	/// When called triggers attack
	/// </summary>
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
			if(((transform.position - enemyHitRay.collider.gameObject.transform.position) / 2f).magnitude > 1)
			{
				characterController.Move(-(transform.position - enemyHitRay.collider.gameObject.transform.position) / 2f);
			}
			
			transform.LookAt(enemyHitRay.collider.gameObject.transform);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			//enemyHitRay.collider.gameObject.GetComponent<HealthController>().Damage(100000);
			IDamageable damageable = (IDamageable) enemyHitRay.collider.gameObject.GetComponent(typeof(IDamageable));
			AttackResponse attackResponse = damageable.Damage(new Attack(15, HealthV2.Attack.AttackType.Heavy));
			if(attackResponse.HitType == AttackResponse.HitResult.Blocked)
			{
				StartCoroutine(StagerPlayer(combatProps.stageredProps.shieldHit));
			}
			else
			{
				GameEvents.current.player.DamageEnemy();
				StartCoroutine(AttackDur());
			}
		}
		if(hitState == HitState.Shield)
		{
			//Stager player
			StartCoroutine(StagerPlayer(combatProps.stageredProps.shieldHit));
		}
	}

	/// <summary>
	/// This method is subscribed to the TakeDamage event of the player health system
	/// </summary>
	/// <param name="currentHealth"></param>
	/// <param name="maxHealth"></param>
	/// <param name="healthDelta"></param>
	private void DamagedStager(float currentHealth, float maxHealth, float healthDelta)
	{
		float stagDur = 0;
		if(healthDelta > 1)
		{
			stagDur = combatProps.stageredProps.smallHit;
		}
		else if(healthDelta > 5)
		{
			stagDur = combatProps.stageredProps.mediumHit;
		}
		StartCoroutine(StagerPlayer(stagDur));
	}

	private IEnumerator AttackDur()
	{
		combatState = PlayerCombatState.Attacking;
		playerController.DeactivateMovment();
		//attack.IsAttacking = true;
		yield return new WaitForSeconds(combatProps.attackDuration);
		//attack.IsAttacking = false;
		playerController.ActivateMovment();
		combatState = PlayerCombatState.None;
	}

	/// <summary>
	/// Method for stagering the player for amount of time
	/// </summary>
	/// <param name="time">length of stager</param>
	/// <returns>IEnumerator that does stager player</returns>
	private IEnumerator StagerPlayer(float time)
	{
		combatState = PlayerCombatState.Stagered;
		playerController.DeactivateMovment();
		yield return new WaitForSeconds(time);
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

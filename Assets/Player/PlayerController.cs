using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using EventSystem;

public class PlayerController : MonoBehaviour
{
	
	[SerializeField]
	private Joystick joystick;

	[SerializeField]
	private PlayerMovmentProps playerMovmentProps;

	[SerializeField]
	private DashSettings dashSettings;

	public ParticleSystem dashParticles;

	public Animator animator;
	public GameObject model;

	public bool movementExact;
	public float moveSpeed;

	private Vector2 direction;
	public Vector2 Direction { get => direction; }

	private Vector2 playerDirection;

	private Rigidbody rb;

	private bool movmentIsActive = true;
	private bool isDashing = false, dashIsReady = true;
	private bool inputBlocked = false;
	private bool inAttack = false;

	private Vector3 dashStartPos, dashEndPos;
	private float dashTimer;

	private CharacterController characterController;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		animator.SetBool("Grounded", true);
		animator.applyRootMotion = true;
	}

	// Update is called once per frame
	void Update()
	{
		animator.SetFloat("StateTime", Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(1).normalizedTime, 1f));
		inAttack = animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack");

		//direction = new Vector2(-Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"));
		direction = new Vector2(-joystick.Vertical + joystick.Horizontal, joystick.Vertical + joystick.Horizontal);
		
		if (direction.magnitude != 0)//if the user is giving direction input
		{
			playerDirection = direction.normalized;
			model.transform.eulerAngles = new Vector3(0, Vector2.SignedAngle(playerDirection, Vector2.up), 0);
		}
		if (movmentIsActive)
		{
			animator.SetFloat("MoveSpeed", new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y)).magnitude);
		}
		else
		{
			animator.SetFloat("MoveSpeed", 0);
		}

		//animator.SetFloat("MoveSpeed", new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y)).magnitude * 1.1f);

		if (movmentIsActive && !inAttack)
		{
			PlayerMovment();
		}
		else
		{
			//rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.2f);
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			TriggerDash();
		}

		if (isDashing)
		{
			DashUpdate();
		}
	}

	private void OnMove(InputValue inputValue)
	{
		Debug.Log("moving");
	}

	void PlayerMovment()
	{
		if (movementExact)
		{
			characterController.Move(new Vector3(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime));
		}
	}

	public void TriggerDash()
	{
		if (!isDashing && dashIsReady)
		{
			StartDash();
		}
	}

	void StartDash()
	{
		isDashing = true;
		Invoke(nameof(EndDash), dashSettings.duration);
		movmentIsActive = false;
		dashStartPos = transform.position;
		dashEndPos = transform.position + (new Vector3(playerDirection.x, 0, playerDirection.y) * dashSettings.distance);
		dashTimer = 0;
		dashParticles.Play();
		animator.SetTrigger("Dash");
		StartCoroutine(DashCoolDown());
	}

	void DashUpdate()
	{
		dashTimer += Time.deltaTime / dashSettings.duration;
		//transform.position = Vector3.Lerp(dashStartPos, dashEndPos, dashTimer);
		
		//This dash code is temporary
		characterController.Move((new Vector3(playerDirection.x, 0, playerDirection.y) * dashSettings.speed) * Time.deltaTime);
	}

	private IEnumerator DashCoolDown()
	{
		dashIsReady = false;
		GameEvents.current.player.AbilityCooldownStart(1);
		yield return new WaitForSeconds(dashSettings.cooldown);
		GameEvents.current.player.AbilityCooldownEnd(1);
		dashIsReady = true;
	}

	void EndDash()
	{
		movmentIsActive = true;
		isDashing = false;
	}

	public void ActivateMovment()
	{
		movmentIsActive = true;
	}

	public void DeactivateMovment()
	{
		movmentIsActive = false;
	}

	/*private void OnAnimatorMove()
	{
		Animator animator = GetComponent<Animator>();

		if (animator)
		{
			Vector3 newPosition = transform.position;
			newPosition += new Vector3(playerDirection.x * animator.GetFloat("AttackMove") * Time.deltaTime, 0, playerDirection.y * animator.GetFloat("AttackMove") * Time.deltaTime);
			transform.position = newPosition;
		}
	}*/
}

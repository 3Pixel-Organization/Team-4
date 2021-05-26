using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using EventSystem;
using UnityEngine.UI;

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

	private float timeSlowAmount = 1.4f;
	private bool timeSlowIsActive = false, canSlowDown = true;

	private bool isSprinting = false;

	private CharacterController characterController;

	private Outline outline;

	[SerializeField] private ProgressBar progressBar;

	[SerializeField] private VolumeFade toSlowmo;
	[SerializeField] private VolumeFade fromSlowmo;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		animator.SetBool("Grounded", true);
		animator.applyRootMotion = true;
		outline = GetComponent<Outline>();
	}

	// Update is called once per frame
	void Update()
	{
		//rb.velocity = Vector3.forward * 10000;
		animator.SetFloat("StateTime", Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(1).normalizedTime, 1f));
		inAttack = animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack");

		Vector2 unRotatedDir = new Vector2(joystick.Horizontal, joystick.Vertical);
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			unRotatedDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			unRotatedDir.Normalize();
		}
		direction = Quaternion.Euler(new Vector3(0, 0, 45)) * unRotatedDir;

		//direction = new Vector2(-Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"));
		//direction = new Vector2(-joystick.Vertical + joystick.Horizontal, joystick.Vertical + joystick.Horizontal);

		
		//Debug.Log(direction.magnitude);
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

		if (Input.GetKeyDown(KeyCode.C))
		{
			TimeSlowPress();
		}

		if (Input.GetKeyUp(KeyCode.C))
		{
			TimeSlowRelease();
		}

		isSprinting = Input.GetKey(KeyCode.LeftShift);
		progressBar.current = Mathf.CeilToInt(timeSlowAmount * 100);
	}

	private void OnMove(InputValue inputValue)
	{
		Debug.Log("moving");
	}

	void PlayerMovment()
	{
		if (movementExact)
		{
			float speedMultiplier = 1;
			if (isSprinting)
			{
				speedMultiplier = 2f;
			}
			characterController.Move(new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime * speedMultiplier);
		}
		//rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);
	}

	public void TriggerDash()
	{
		if (!isDashing && dashIsReady)
		{
			StartDash();
		}
	}
	public void TimeSlowPress()
	{
		if (!canSlowDown) return;
		StartTimeSlow();
		StartCoroutine(TimeSlowTimer());
	}

	public void TimeSlowRelease()
	{
		if (timeSlowIsActive)
		{
			StopCoroutine(TimeSlowTimer());
			StopTimeSlow();
			StartDash();
			timeSlowAmount = 0;
		}
	}

	void StartTimeSlow()
	{
		timeSlowIsActive = true;
		movmentIsActive = false;
		Time.timeScale = 0.1f;
		outline.enabled = true;
		toSlowmo.DoEffect();
	}

	void StopTimeSlow()
	{
		timeSlowIsActive = false;
		movmentIsActive = true;
		Time.timeScale = 1f;
		outline.enabled = false;
		fromSlowmo.DoEffect();
		StartCoroutine(nameof(TimeSlowCooldown));
	}

	IEnumerator TimeSlowTimer()
	{
		while (timeSlowAmount > 0)
		{
			timeSlowAmount -= Time.deltaTime/0.1f;
			yield return new WaitForEndOfFrame();
		}
		StopTimeSlow();
	}

	IEnumerator TimeSlowCooldown()
	{
		canSlowDown = false;
		yield return new WaitForSecondsRealtime(0.5f);
		canSlowDown = true;
		timeSlowAmount = 1.4f;
	}

	public void SkipTimeSlowCool()
	{
		StopCoroutine(TimeSlowCooldown());
		canSlowDown = true;
		timeSlowAmount = 1.4f;
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

	public void MoveTo(Vector3 position)
	{
		characterController.enabled = false;
		transform.position = position;
		characterController.enabled = true;
	}

	public IEnumerator DashCoolDown()
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

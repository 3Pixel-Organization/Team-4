using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	
	[SerializeField]
	private Joystick joystick;

	[SerializeField]
	private PlayerSettings playerSettings;

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
	private bool isDashing = false;
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
		
		inAttack = animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack");

		//direction = new Vector2(-Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"));
		direction = new Vector2(-joystick.Vertical + joystick.Horizontal, joystick.Vertical + joystick.Horizontal);
		
		if (direction.magnitude != 0)//if the user is giving direction input
		{
			playerDirection = direction.normalized;
			model.transform.eulerAngles = new Vector3(0, Vector2.SignedAngle(playerDirection, Vector2.up), 0);
		}
		animator.SetFloat("MoveSpeed", new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y)).magnitude);

		//animator.SetFloat("MoveSpeed", new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y)).magnitude * 1.1f);

		if (movmentIsActive && !inAttack)
		{
			PlayerMovment();
		}
		else
		{
			//rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.2f);
		}

		if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing)
		{
			StartDash();
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
			//rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);
			characterController.Move(new Vector3(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime));
			return;
		}

		Vector3 velocityToAdd = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * playerSettings.movementSpeed;
		rb.velocity += velocityToAdd;
		rb.velocity -= Vector3.Lerp(Vector3.zero, rb.velocity, playerSettings.moveDampening * Time.deltaTime);

		float totalSpeed = new Vector2(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.z)).magnitude;

		if (totalSpeed >= playerSettings.maxSpeed)
		{
			float difference = playerSettings.maxSpeed / totalSpeed;
			rb.velocity = new Vector3(rb.velocity.x * difference, rb.velocity.y, rb.velocity.z * difference);
		}
		if (Input.GetKeyDown(KeyCode.Space))//jumping currently deactivated
		{
			rb.AddForce(Vector3.up * playerSettings.jumpForce);
		}
	}

	public void TriggerDash()
	{
		if (!isDashing)
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
	}

	void DashUpdate()
	{
		dashTimer += Time.deltaTime / dashSettings.duration;
		transform.position = Vector3.Lerp(dashStartPos, dashEndPos, dashTimer);
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

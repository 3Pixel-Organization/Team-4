using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private PlayerSettings playerSettings;

	[SerializeField]
	private DashSettings dashSettings;

	private Vector2 direction;
	public Vector2 Direction { get => direction; }

	private Vector2 playerDirection;

	private Rigidbody rb;

	private bool movmentIsActive = true;
	private bool isDashing = false;

	private Vector3 dashStartPos, dashEndPos;
	private float dashTimer;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();	
	}

	// Update is called once per frame
	void Update()
	{
		direction = new Vector2(-Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"));
		if(direction.magnitude != 0)//if the user is giving direction input
		{
			playerDirection = direction.normalized;
		}

		if (movmentIsActive)
		{
			PlayerMovment();
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

	void PlayerMovment()
	{
		Vector3 velocityToAdd = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * playerSettings.movementSpeed;
		rb.velocity += velocityToAdd;
		rb.velocity -= Vector3.Lerp(Vector3.zero, rb.velocity, playerSettings.moveDampening * Time.deltaTime);
		float totalSpeed = new Vector2(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.z)).magnitude;

		if (totalSpeed >= playerSettings.maxSpeed)
		{
			float difference = playerSettings.maxSpeed / totalSpeed;
			rb.velocity = new Vector3(rb.velocity.x * difference, rb.velocity.y, rb.velocity.z * difference);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector3.up * playerSettings.jumpForce);
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
}

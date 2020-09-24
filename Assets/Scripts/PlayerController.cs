using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
	public PlayerSettings playerSettings;

	public Vector2 direction;

	private Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();	
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity += new Vector3(-Input.GetAxisRaw("Vertical") * Time.deltaTime * playerSettings.movementSpeed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * playerSettings.movementSpeed);
		rb.velocity += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * playerSettings.movementSpeed, 0, Input.GetAxisRaw("Horizontal") * Time.deltaTime * playerSettings.movementSpeed);
		rb.velocity -= Vector3.Lerp(Vector3.zero, rb.velocity, playerSettings.moveDampening * Time.deltaTime);
		float totalSpeed = new Vector2(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.z)).magnitude;
		
		if(totalSpeed >= playerSettings.maxSpeed)
		{
			float difference = playerSettings.maxSpeed/ totalSpeed;
			rb.velocity = new Vector3(rb.velocity.x * difference, rb.velocity.y, rb.velocity.z * difference);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector3.up * playerSettings.jumpForce);
		}
		//rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -playerSettings.maxSpeed, playerSettings.maxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -playerSettings.maxSpeed, playerSettings.maxSpeed));
	}
}

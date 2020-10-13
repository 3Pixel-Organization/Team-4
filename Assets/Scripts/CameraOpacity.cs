using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 
public class RayCastTransparency : MonoBehaviour
{
	/*
	public Transform raytarget; // Create a "field" for the raytarget (ie the camera) to be placed

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Declaring variables that are going to be used
		int layermask = 1 << 10; // bitshift index of the layer to get a bit mask (?)
		RaycastHit hitWall; // where the hit target is going to be stored

		Vector3 toCamera; // create the Vector3 we're going to use
		toCamera = raytarget.position - transform.position; // transform.position is the xyz coordinate inside the room of the calling object... so subtracting the target gives us a VECTOR! yay!

		Debug.DrawRay(transform.position, toCamera, Color.green, 10f); // Draw the ray

		Ray checkRay = new Ray(transform.position, toCamera); // Create the ray variable to be used in the next line
		if (Physics.Raycast(checkRay, out hitWall, 10, layermask))
		{
			//Debug.Log("Hitting a wall, Stage Start");
			HighWall highwall = hitWall.transform.GetComponent<HighWall>();
			if (highwall != null)
			{
				highwall.FadeOut(0.6f);
				//Debug.Log("Hitting a wall, Stage Final");
			}
		}

	}*/
}

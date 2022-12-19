using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunerKnob : MonoBehaviour 
{
	[HideInInspector]
	public float rotationSpeed;

	private Transform tunerIndicator;
	
	private float frequency = 0f;
	private float startingRotation;

	private float minRotation = -0.9f;
	private float maxRotation = 0.85f;

	private float tunerSpeed;

	

	// Use this for initialization
	void Start () 
	{
		// Find the tuner indicator transform
		tunerIndicator = GameObject.Find ("Arrow").transform;

		// Set the proper ratio of rotation from
		// the tuner knob to the indicator
		//tunerSpeed = Mathf.Round (rotationSpeed * 0.6667f);
		SetSpeed (rotationSpeed);

		// Capture our starting rotation
		startingRotation = transform.localRotation.z;
	}

	// Check for right click and left
	// click on the 
	void OnMouseOver()
	{
		// Get the rotation of the
		// transform
		float zRot = transform.localRotation.z;

		if(Input.GetKey (KeyCode.Mouse0))
		{

			// make sure zRot is within our
			// allowed range
			if(zRot > maxRotation)
				return;

			// R
			transform.Rotate (Vector3.forward * Time.deltaTime * rotationSpeed);
			tunerIndicator.Rotate(Vector3.forward * Time.deltaTime * tunerSpeed);
		}

		else if(Input.GetKey (KeyCode.Mouse1))
		{
			// Make sure zRot is in the
			// desired range
			if(zRot < minRotation)
				return;

			transform.Rotate (Vector3.back * Time.deltaTime * rotationSpeed);
			tunerIndicator.Rotate(Vector3.back * Time.deltaTime * tunerSpeed);		
		}


		if(frequency != (zRot - startingRotation))
			frequency = (zRot - startingRotation);

	}

	public float GetFrequency()
	{
		return frequency;
	}

	public void SetSpeed(float speed)
	{
		rotationSpeed = speed;
		tunerSpeed = rotationSpeed * 0.6667f;
	}
}

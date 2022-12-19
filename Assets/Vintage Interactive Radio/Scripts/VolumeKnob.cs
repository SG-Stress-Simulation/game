using UnityEngine;
using System.Collections;

public class VolumeKnob : MonoBehaviour 
{
	[HideInInspector]
	public AudioSource controlledAudio;
	[HideInInspector]
	public float rotationSpeed;

	private float knobRotation = 0f;
	private float volumeModifier = 0.57f;
	private float startingRotation;


	void Start()
	{
		// Get a reference to the starting
		// rotation of this dial
		startingRotation = transform.localRotation.z;
	}

	void OnMouseOver()
	{
		// Get the current rotation of the
		// transform 
		float zRot = transform.localRotation.z;

		// Left click handling
		if(Input.GetKey (KeyCode.Mouse0))
		{
			// Rotate the dial to the right
			if(controlledAudio.volume < 1.0f)
				transform.Rotate (Vector3.forward * Time.deltaTime * rotationSpeed);
		}


		// Right click handling
		else if(Input.GetKey (KeyCode.Mouse1))
		{
			// Rotate the dial to the left
			if(controlledAudio.volume > 0f)
				transform.Rotate (Vector3.back * Time.deltaTime * rotationSpeed);	
		}

		// Update the volume of the audio
		// source that is being controlled
		if(knobRotation != (zRot - startingRotation))
		{
			knobRotation = (zRot - startingRotation);
			controlledAudio.volume = knobRotation * volumeModifier;
		}
	}
}

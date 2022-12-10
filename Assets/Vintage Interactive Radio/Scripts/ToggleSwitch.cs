using UnityEngine;
using System.Collections;

public class ToggleSwitch : MonoBehaviour {
	
	[HideInInspector]
	public AudioSource controlledAudio;

	private bool toggleState = false;
		
	void Start () 
	{
		// Set up our toggle switch to
		// be in the "off" position
		transform.localEulerAngles = new Vector3(-45f, 0f, 0f);
		CheckPlay();
	}

	void OnMouseUpAsButton()
	{
		ToggleTwoState ();
		CheckPlay ();
	}
	
	void ToggleTwoState()
	{
		toggleState = !toggleState;

		// Rotate the switch to the
		// correct position based on
		// the value of toggle state
		transform.localEulerAngles = toggleState ?
			new Vector3(45f, 0f, 0f):
				new Vector3(-45f, 0f, 0f);
	}
	
	void CheckPlay()
	{
		// Mute the controlled audio source
		if(controlledAudio != null)
			controlledAudio.mute = toggleState;
	}
}

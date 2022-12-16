using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class InteractiveRadio : MonoBehaviour 
{
	private VolumeKnob volumeKnob;
	private TunerKnob tunerKnob;
	private ToggleSwitch toggleSwitch;

	[Range(10f, 50f)]
	public float volumeRotationSpeed = 25f;
	[Range(10f, 50f)]
	public float tunerRotationSpeed = 25f;

	public List<RadioStation> radioStations;
	public AudioClip whiteNoise;


	// Use this for initialization
	void Start () 
	{
		// Starting volume of our audio
		// source is 0
		GetComponent<AudioSource>().volume = 0f;

		// Get references to all the controls
		volumeKnob = GetComponentInChildren<VolumeKnob>();
		tunerKnob = GetComponentInChildren<TunerKnob>();
		toggleSwitch = GetComponentInChildren<ToggleSwitch>();

		// Pass the audio source to the
		// controls for reference
		volumeKnob.controlledAudio = GetComponent<AudioSource>();
		toggleSwitch.controlledAudio = GetComponent<AudioSource>();

		// Set the starting rotation speeds
		volumeKnob.rotationSpeed = volumeRotationSpeed;
		tunerKnob.SetSpeed (tunerRotationSpeed);

		// Update the radio station
		UpdateStation (tunerKnob.GetFrequency ());
	}

	void Update()
	{
		// Update the speed of our controls if
		// they have been changed
		if(volumeKnob.rotationSpeed != volumeRotationSpeed)
			volumeKnob.rotationSpeed = volumeRotationSpeed;

		if(tunerKnob.rotationSpeed != tunerRotationSpeed)
			tunerKnob.SetSpeed (tunerRotationSpeed);

		// Update our station if necessary
		UpdateStation (tunerKnob.GetFrequency());
	}

	// This function returns the currently
	// tuned radio station
	RadioStation TunedStation(float freq)
	{
		// Loop through our radio stations
		// to get the current station
		foreach(RadioStation rs in radioStations)
		{
			if(rs.isTuned (freq))
				return rs;
		}
		
		return null;
	}

	// This function updates the audio clip
	// with the appropriate one based on
	// the currently tuned frequency
	void UpdateStation(float frequency)
	{
		
		// Get the station at "frequency"
		RadioStation targetStation = TunedStation(frequency);
		
		// If there is no station at
		// that frequency, play the white
		// noise sound if it is available
		if(targetStation == null)
		{
			// Check for white noise
			if(whiteNoise != null)
			{
				// The white noise is playing or
				// the clip is null exit
				if(GetComponent<AudioSource>().clip == whiteNoise)
					return;

				// Stop audio
				GetComponent<AudioSource>().Stop ();
				GetComponent<AudioSource>().clip = whiteNoise;
				GetComponent<AudioSource>().Play ();
			}

			// If there is no white noise
			// available clear the audio source
			// clip
			else
			{
				GetComponent<AudioSource>().Stop ();
				GetComponent<AudioSource>().clip = null;
			}


		}
		
		// Otherwise, stop the current clip and
		// play the audio clip associated 
		// with the station
		else
		{
			if(GetComponent<AudioSource>().clip != targetStation.clip)
			{
				GetComponent<AudioSource>().Stop ();
				GetComponent<AudioSource>().clip = targetStation.clip;
				GetComponent<AudioSource>().Play ();
			}
		}
	}

}

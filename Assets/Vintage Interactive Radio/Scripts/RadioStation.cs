using UnityEngine;
using System.Collections;

[System.Serializable]
public class RadioStation
{
	[SerializeField]
	public float minFrequency;
	[SerializeField]
	public float maxFrequency;
	[SerializeField]
	public AudioClip clip;

	public bool isTuned(float frequency)
	{
		return(frequency < maxFrequency && frequency > minFrequency);
	}

}

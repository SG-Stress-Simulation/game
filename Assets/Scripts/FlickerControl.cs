using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{

    public bool isFlickering = false;
    public float timeDelay;
    public float flickeringDelayOff;
    public float flickeringDelayOn;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false) {
            StartCoroutine(FlickeringLight());
        }

    }

    IEnumerator FlickeringLight(){
        audioSource.Play();
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
	    timeDelay = Random.Range(0.01f, flickeringDelayOff);
        yield return new WaitForSeconds(timeDelay);

        audioSource.Play();
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.01f, flickeringDelayOn);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}

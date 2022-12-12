using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{

    public bool isFlickering = false;
    public float timeDelay;
    public float flickeringDelayOff;
    public float flickeringDelayOn;
    public AudioSource audioSourceOn;
    public AudioSource audioSourceOff;

    public AudioSource buzzing;

    public Material matOff;
    public Material matOn;

    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false) {
            StartCoroutine(FlickeringLight());
        }

    }

    IEnumerator FlickeringLight(){
        audioSourceOff.Play();
        buzzing.Stop();
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().material = matOff;
        timeDelay = Random.Range(0.01f, flickeringDelayOff);
        yield return new WaitForSeconds(timeDelay);

        audioSourceOn.Play();
        buzzing.Play();
        this.gameObject.GetComponent<Light>().enabled = true;
        this.gameObject.GetComponent<MeshRenderer>().material = matOn;
        timeDelay = Random.Range(0.01f, flickeringDelayOn);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}

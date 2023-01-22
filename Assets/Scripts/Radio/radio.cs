using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class radio : MonoBehaviour
{
    public BooleanAction triggerPressed;

    public AudioClip radioNoise;
    private AudioSource audio;

    public GameObject hand;

    public Collider collider;
    private bool state;


    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 handPos = hand.transform.position;

        if (Input.GetKeyDown(KeyCode.L) && collider.bounds.Contains(handPos))
        {
            if(state)
            {
                audio.Stop();
                state= false;
            } else
            {
                audio.clip = radioNoise;
                audio.Play();
                state = true;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireCracker : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip fuse;
    public AudioClip explosion;
    public AudioClip tinnitus;

    public CanvasGroup AlphaController;

    private Vector3 startPos;
    private Quaternion startRot;
    private AudioSource[] myAudioSources;
    private bool on = false;
    private bool activated = false;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        myAudioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(on)
        {
            AlphaController.alpha = AlphaController.alpha - Time.deltaTime * 1f;
            myAudioSources[1].volume = myAudioSources[1].volume - Time.deltaTime * 0.2f;

            if (myAudioSources[1].volume <= 0 ) 
            {
                AlphaController.alpha = 0;
                myAudioSources[1].volume = 1;
                myAudioSources[1].Stop();
                on = false;
            }
        }

        if(transform.position.y < 1.5f && activated)
        {
            myAudioSources[0].Stop();
            myAudioSources[0].clip = explosion;
            myAudioSources[1].clip = tinnitus;
            myAudioSources[1].Play();
            myAudioSources[0].Play();

            AlphaController.alpha = 1;

            on = true;
            activated= false;

            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            activated = true;
            myAudioSources[0].clip = fuse;
            myAudioSources[0].Play();
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));            
        }
    }
}

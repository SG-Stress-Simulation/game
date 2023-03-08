using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3AmbiSound : MonoBehaviour
{


    [Header("Random Audio Clips")]
    public AudioClip clip1;
    public AudioClip clip2;

    [Header("Debounce")]
    public float debounceTime = 20f;
    private float debounceTimer = 0f;

    private AudioSource[] myAudioSources;

    private float number = 0;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (debounceTimer > 0f)
        {
            debounceTimer -= Time.deltaTime;
            return;
        }

        number = Random.value;

        if(number < 0.5)
        {
            myAudioSources[1].clip = clip1;
        } else
        {
            myAudioSources[1].clip = clip2;
        }

        myAudioSources[1].volume = Random.value;
        myAudioSources[1].Play();

        debounceTimer = 10 + debounceTime * Random.value;
    }
}

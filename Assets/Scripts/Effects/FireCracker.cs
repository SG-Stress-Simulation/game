using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zinnia.Action.Effects;


public class FireCracker : BaseEffect
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
    
    public override void StopEffect()
    {
        base.StopEffect();
    }

    public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
    {
        base.StartEffect(oneShot, effectDuration, effectIntensity);
        myAudioSources[0].clip = fuse;
        myAudioSources[0].Play();
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));    
    }

    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        startRot = transform.rotation;
        myAudioSources = GetComponents<AudioSource>();
    }
        
    public override void Update()
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

        if(transform.position.y < 1.5f && effectRunning)
        {
            myAudioSources[0].Stop();
            myAudioSources[0].clip = explosion;
            myAudioSources[1].clip = tinnitus;
            myAudioSources[1].Play();
            myAudioSources[0].Play();

            AlphaController.alpha = 1;

            on = true;
            effectRunning = false;

            transform.position = startPos;
            transform.rotation = startRot;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Zinnia.Action.Effects;


public class FireCracker : BaseEffect
{
    [Header("Audio Clips")]
    public AudioClip fuse;
    public AudioClip explosion;
    public AudioClip tinnitus;

    public CanvasGroup AlphaController;

    private ColorGrading cg;

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
        cg = ScriptableObject.CreateInstance<ColorGrading>();
        cg.enabled.Override(true);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, cg);
    }
        
    public override void Update()
    {
        if(on)
        {
            AlphaController.alpha = AlphaController.alpha - Time.deltaTime * 1f;
            while (cg.brightness.value > 0f)
            {
                cg.brightness.Override(cg.brightness.value - Time.deltaTime * 10f);
            }
            if(cg.brightness.value <= 0f) cg.brightness.Override(0f);
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
            cg.brightness.Override(100f);

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

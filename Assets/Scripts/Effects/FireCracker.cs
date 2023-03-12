using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zinnia.Action.Effects;
using System.Collections.Generic;


public class FireCracker : BaseEffect
{
    [Header("Audio Clips")]
    public AudioClip fuse;
    public AudioClip explosion;
    public AudioClip tinnitus;

    [Header("Firecracker Prefab")]
    public GameObject fireCrackerPrefab;

    private ColorGrading cg;
    private List<GameObject> activeFireCrackers;
    private AudioSource tinnitusAudioSrc;
    private AudioSource fuseAudioSrc;

    private float brightness;

    public override void StopEffect()
    {
        base.StopEffect();
    }

    public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
    {
        base.StartEffect(oneShot, effectDuration, effectIntensity);
        fuseAudioSrc.Play();
        GameObject fireCracker = Instantiate(fireCrackerPrefab, transform.position, Quaternion.identity);
        fireCracker.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));
        activeFireCrackers.Add(fireCracker);
    }

    public override void Start()
    {
        base.Start();

        // create audio sources
        tinnitusAudioSrc = gameObject.AddComponent<AudioSource>();
        tinnitusAudioSrc.spatialBlend = 0f;
        tinnitusAudioSrc.clip = tinnitus;
        fuseAudioSrc = gameObject.AddComponent<AudioSource>();
        fuseAudioSrc.spatialBlend = 1f;
        fuseAudioSrc.clip = fuse;

        // create flashbang effect
        cg = ScriptableObject.CreateInstance<ColorGrading>();
        cg.gradingMode.Override(GradingMode.LowDefinitionRange);
        cg.enabled.Override(true);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 102f, cg);

        brightness = 0f;
        
        // create list of active firecrackers
        activeFireCrackers = new List<GameObject>();
    }
        
    public override void Update()
    {
        if (brightness > 0f)
            brightness -= Time.deltaTime * 30f;
        else
            brightness = 0f;
        
        if (tinnitusAudioSrc.volume > 0f)
        {
            tinnitusAudioSrc.volume = Math.Max(tinnitusAudioSrc.volume - Time.deltaTime * 0.2f, 0f);
        }

        // iterate over active firecrackers
        for (int i = activeFireCrackers.Count - 1; i > -1; i--)
        {
            GameObject explosive = activeFireCrackers[i];
            if (explosive.transform.position.y < 1.5f)
            {
                // stat effects
                brightness = 100f;
                tinnitusAudioSrc.volume = 1f;
                // hide explosive - disable mesh renderer
                explosive.GetComponent<MeshRenderer>().enabled = false;
                // play explosion sound
                AudioSource audioSrc = explosive.AddComponent<AudioSource>();
                audioSrc.spatialBlend = 1f;
                audioSrc.clip = explosion;
                audioSrc.Play();
                // after explosion, remove the firecracker (2 seconds after explosion)
                Destroy(explosive, 2f);
                // remove firecracker from active list
                activeFireCrackers.RemoveAt(i);
            }
        }

        if (activeFireCrackers.Count == 0)
        {
            effectRunning = false;
        }
        
        cg.contrast.Override(brightness);
        cg.brightness.Override(brightness);
        cg.lift.Override(new Vector4(1f,1f,1f, (brightness / 50)));
        
        
        base.Update();
    }
}

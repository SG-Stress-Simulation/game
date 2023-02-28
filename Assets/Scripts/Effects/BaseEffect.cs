using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zinnia.Action.Effects;
using Random = UnityEngine.Random;

public class BaseEffect : MonoBehaviour
{
    public int timeToEffectEndMin = 3;
    public int timeToEffectEndMax = 5;
    [HideInInspector]
    public float timeToEffectEnd;
    [HideInInspector]
    public float duration = 0.0f;
    public bool test = false;
    [HideInInspector]
    public bool loop = false;
    [HideInInspector]
    public bool effectRunning = false;

    [HideInInspector] public float intensity = EffectDefaults.EFFECT_INTENSITY;
    
    [HideInInspector]
    public PostProcessVolume m_Volume;

    public virtual void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
    {
        Debug.Log("Started Effect");
        if(effectDuration > 0.0f)
        {
            timeToEffectEnd = effectDuration;
        }
        else
        {
            timeToEffectEnd = Random.Range(timeToEffectEndMin, timeToEffectEndMax) * Mathf.PI;
        }
        duration = timeToEffectEnd;
        intensity = effectIntensity;
        loop = !oneShot;
        effectRunning = true;
        Debug.Log("Duration " + duration);
    }
    
    public virtual void StopEffect()
    {
        Debug.Log("Stopped Effect");
        loop = false;
        effectRunning = false;
    }
    
    public virtual void UpdateEffect()
    {
    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if(test || loop) effectRunning = true;
        if (timeToEffectEnd <= 0 && !(test || loop) && effectRunning) StopEffect();
        if (timeToEffectEnd <= 0 && (test || loop)) timeToEffectEnd = Random.Range(timeToEffectEndMin, timeToEffectEndMax);
        timeToEffectEnd -= Time.deltaTime;
    }
    
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
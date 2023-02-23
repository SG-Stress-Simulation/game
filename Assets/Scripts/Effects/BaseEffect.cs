using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
    public bool effectRunning = false;
    
    [HideInInspector]
    public PostProcessVolume m_Volume;

    public virtual void StartEffect()
    {
        Debug.Log("Started Effect");
        timeToEffectEnd = Random.Range(timeToEffectEndMin, timeToEffectEndMax) * Mathf.PI;
        duration = timeToEffectEnd;
        effectRunning = true;
        Debug.Log("Duration " + duration);
    }
    
    public virtual void StopEffect()
    {
        Debug.Log("Stopped Effect");
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
        if(test) effectRunning = true;
        if (timeToEffectEnd <= 0 && !test && effectRunning) StopEffect();
        if (timeToEffectEnd <= 0 && test) timeToEffectEnd = Random.Range(timeToEffectEndMin, timeToEffectEndMax);
        timeToEffectEnd -= Time.deltaTime;
    }
    
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
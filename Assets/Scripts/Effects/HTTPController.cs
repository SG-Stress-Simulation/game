using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action.Effects;

enum NextEffect
{
    NONE,
    COLOR_LOSS,
    DOF,
    VIGNETTE,
    STOP_EFFECTS
}

public class HTTPController : MonoBehaviour
{
    public ColorLossEffect colorLossEffect;
    public VignetteEffect vignetteEffect;
    public DOF dofEffect;
    private NextEffect nextEffect = NextEffect.NONE;
    public bool inifiniteEffectRunning = false;
    public bool nextEffectInfinte = false;
    public float nextEffectDuration = -0.1f;
    public float nextEffectIntensity = 0.75f;

public ReturnResult StartVignette(bool infinite = EffectDefaults.EFFECT_INFINITE, float duration = EffectDefaults.EFFECT_DURATION, float intensity = EffectDefaults.EFFECT_INTENSITY)
    {
        nextEffect = NextEffect.VIGNETTE;
        nextEffectInfinte = infinite;
        nextEffectDuration = duration;
        nextEffectIntensity = intensity;
        return StandardResult("VIGNETTE");
    }
    
    public ReturnResult StartDOF(bool infinite = EffectDefaults.EFFECT_INFINITE, float duration = EffectDefaults.EFFECT_DURATION, float intensity = EffectDefaults.EFFECT_INTENSITY)
    {
        nextEffect = NextEffect.DOF;
        nextEffectInfinte = infinite;
        nextEffectDuration = duration;
        nextEffectIntensity = intensity;
        return StandardResult("DOF");
    }

    public ReturnResult StartColorLoss(bool infinite = EffectDefaults.EFFECT_INFINITE, float duration = EffectDefaults.EFFECT_DURATION, float intensity = EffectDefaults.EFFECT_INTENSITY)
    {
        nextEffect = NextEffect.COLOR_LOSS;
        nextEffectInfinte = infinite;
        nextEffectDuration = duration;
        nextEffectIntensity = intensity;
        return StandardResult("COLOR_LOSS");
    }
    
    public ReturnResult StopEffects()
    {
        nextEffect = NextEffect.STOP_EFFECTS;
        return StandardResult("STOP EFFECTS");
    }
    
    public ReturnResult Health()
    {
        ReturnResult result = new ReturnResult
        {
            code = 200,
            msg = "up",
            showStopButton = inifiniteEffectRunning,
        };
        return result;
    }

    private ReturnResult StandardResult(string effectName)
    {
        
        ReturnResult result = new ReturnResult
        {
            code = 200,
            msg = effectName + " effect started",
            showStopButton = inifiniteEffectRunning,
        };
        return result;
    }
    
    //Mark as Serializable to make Unity's JsonUtility works.
    [System.Serializable]
    public class ReturnResult
    {
        public string msg;
        public int code;
        public bool showStopButton;
    }

    public void Update()
    {
        switch (nextEffect)
        {
            case NextEffect.VIGNETTE:
                vignetteEffect.StartEffect(!nextEffectInfinte);
                break;
            case NextEffect.DOF:
                dofEffect.StartEffect(!nextEffectInfinte);
                break;
            case NextEffect.COLOR_LOSS:
                colorLossEffect.StartEffect(!nextEffectInfinte);
                break;
            case NextEffect.STOP_EFFECTS:
                vignetteEffect.StopEffect();
                dofEffect.StopEffect();
                colorLossEffect.StopEffect();
                break;
        }

        nextEffectInfinte = false;
        nextEffect = NextEffect.NONE;
        nextEffectDuration = 4 * Mathf.PI;
    }
}

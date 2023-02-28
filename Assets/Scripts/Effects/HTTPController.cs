using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Zinnia.Action.Effects;
using Zinnia.Data.Type.Transformation.Conversion;

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

public ReturnResult StartVignette(bool infinite = EffectDefaults.EFFECT_INFINITE, string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.VIGNETTE;
        nextEffectInfinte = infinite;
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
        return StandardResult("VIGNETTE");
    }
    
    public ReturnResult StartDOF(bool infinite = EffectDefaults.EFFECT_INFINITE, string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.DOF;
        nextEffectInfinte = infinite;
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
        return StandardResult("DOF");
    }

    public ReturnResult StartColorLoss(bool infinite = EffectDefaults.EFFECT_INFINITE, string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.COLOR_LOSS;
        nextEffectInfinte = infinite;
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
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
                vignetteEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                break;
            case NextEffect.DOF:
                dofEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                break;
            case NextEffect.COLOR_LOSS:
                colorLossEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
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

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
    STOP_EFFECTS,
    FIRECRACKER,
    COOLDOWN
}

public class HTTPController : MonoBehaviour
{
    public ColorLossEffect colorLossEffect;
    public BlurnetteEffect blurnetteEffect;
    public DOF dofEffect;
    public GuidReference fireCracker;
    private NextEffect nextEffect = NextEffect.NONE;
    public bool inifiniteEffectRunning = false;
    public bool nextEffectInfinte = false;
    public float nextEffectDuration = -0.1f;
    public float nextEffectIntensity = 0.75f;

    private float cooldownTime;

    public ReturnResult StartVignette(string infinite = "false", string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.VIGNETTE;
        nextEffectInfinte = Boolean.Parse(infinite);
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
        return StandardResult("VIGNETTE");
    }
    
    public ReturnResult StartDOF(string infinite = "false", string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.DOF;
        nextEffectInfinte = Boolean.Parse(infinite);
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
        return StandardResult("DOF");
    }

    public ReturnResult StartColorLoss(string infinite = "false", string duration = "-1.0", string intensity = "0.75")
    {
        nextEffect = NextEffect.COLOR_LOSS;
        nextEffectInfinte = Boolean.Parse(infinite);
        nextEffectDuration = float.Parse(duration, CultureInfo.InvariantCulture.NumberFormat);
        nextEffectIntensity = float.Parse(intensity, CultureInfo.InvariantCulture.NumberFormat);;
        return StandardResult("COLOR_LOSS");
    }
    
    public ReturnResult StopEffects()
    {
        nextEffect = NextEffect.STOP_EFFECTS;
        return StandardResult("STOP EFFECTS");
    }

    public ReturnResult StartFirecracker()
    {
        nextEffect = NextEffect.FIRECRACKER;
        return StandardResult("FIRECRACKER");
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
                blurnetteEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                break;
            case NextEffect.DOF:
                dofEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                break;
            case NextEffect.COLOR_LOSS:
                colorLossEffect.StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                break;
            case NextEffect.FIRECRACKER:
                if (fireCracker == null || fireCracker.gameObject == null || !fireCracker.gameObject.activeInHierarchy)
                    break;
                fireCracker.gameObject.GetComponent<FireCracker>().StartEffect(!nextEffectInfinte, nextEffectDuration, nextEffectIntensity);
                nextEffect = NextEffect.COOLDOWN;
                cooldownTime = 3.0f;
                break;
            case NextEffect.COOLDOWN:
                if (cooldownTime > 0.0f)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    blurnetteEffect.StartEffect(true, 20.0f, 1.0f);
                    nextEffect = NextEffect.NONE;
                }
                break;
            case NextEffect.STOP_EFFECTS:
                blurnetteEffect.StopEffect();
                dofEffect.StopEffect();
                colorLossEffect.StopEffect();
                break;
        }

        nextEffectInfinte = false;
        if(nextEffect != NextEffect.COOLDOWN)
            nextEffect = NextEffect.NONE;
        nextEffectDuration = 4 * Mathf.PI;
    }
}

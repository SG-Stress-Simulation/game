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
    VIGNETTE
}

public class HTTPController : MonoBehaviour
{
    public ColorLossEffect colorLossEffect;
    public VignetteEffect vignetteEffect;
    public DOF dofEffect;
    private NextEffect nextEffect = NextEffect.NONE;

    public ReturnResult StartVignette()
    {
        nextEffect = NextEffect.VIGNETTE;
        return StandardResult("VIGNETTE");
    }
    
    public ReturnResult StartDOF()
    {
        nextEffect = NextEffect.DOF;
        return StandardResult("DOF");
    }
    
    public ReturnResult StartColorLoss()
    {
        nextEffect = NextEffect.COLOR_LOSS;
        return StandardResult("COLOR LOSS");
    }

    private ReturnResult StandardResult(string effectName)
    {
        
        ReturnResult result = new ReturnResult
        {
            code = 200,
            msg = effectName + " effect started"
        };
        return result;
    }
    
    //Mark as Serializable to make Unity's JsonUtility works.
    [System.Serializable]
    public class ReturnResult
    {
        public string msg;
        public int code;
    }

    public void Update()
    {
        switch (nextEffect)
        {
            case NextEffect.VIGNETTE:
                vignetteEffect.StartEffect();
                nextEffect = NextEffect.NONE;
                break;
            case NextEffect.DOF:
                dofEffect.StartEffect();
                nextEffect = NextEffect.NONE;
                break;
            case NextEffect.COLOR_LOSS:
                colorLossEffect.StartEffect();
                nextEffect = NextEffect.NONE;
                break;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(BlurnetteRenderer), PostProcessEvent.AfterStack, "Custom/Blurnette")]
public sealed class Blurnette : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Falloff (narrowness).")]
    public FloatParameter narrowness = new FloatParameter { value = 0.5f };

    [Range(0.5f, 2f), Tooltip("Intensity.")]
    public FloatParameter intensity = new FloatParameter { value = 16f };
}
public sealed class BlurnetteRenderer : PostProcessEffectRenderer<Blurnette>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/Blurnette"));
        sheet.properties.SetFloat("Falloff", settings.narrowness);
        sheet.properties.SetFloat("Intensity", settings.intensity);
        // set offset for currently rendered eye
        var eyeOffset = context.xrActiveEye == 1 ? 0.09f : -0.09f;
        sheet.properties.SetFloat("EyeOffset", eyeOffset);        

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
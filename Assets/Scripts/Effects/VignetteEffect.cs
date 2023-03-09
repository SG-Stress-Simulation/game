using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class VignetteEffect : BaseEffect
    {
        Vignette m_Vignette;
        MotionBlur m_Blur;
        AudioSource[] m_AudioSources;
        private float timeToSecond = 0.0f;
        
        public override void StopEffect()
        {
            base.StopEffect();
            m_Vignette.intensity.Override( 0f);
            m_Blur.shutterAngle.Override(0f);
            foreach (var source in m_AudioSources)
            {
                source.Stop();
            }
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
            m_Vignette.intensity.Override(0f);
            foreach (var source in m_AudioSources)
            {
                source.Play();
            }
        }

        public override void Start()
        {
            base.Start();
            m_Vignette = ScriptableObject.CreateInstance<Vignette>();
            m_AudioSources = GetComponents<AudioSource>();
            m_Vignette.enabled.Override(true);
            m_Vignette.color.Override(new Color(0.0f,0.0f,0.0f, 0.5f));

            m_Blur = ScriptableObject.CreateInstance<MotionBlur>();
            m_Blur.enabled.Override(true);
            m_Blur.shutterAngle.Override(0.0f);
            m_Blur.sampleCount.Override(16);
            
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette, m_Blur);
        }

        public override void Update()
        {
            if (timeToSecond >= 0.75f)
            {
                timeToSecond = 0.0f;
            }

            timeToSecond += Time.deltaTime;
            if (effectRunning)
            {
                float overAllIntensity = Mathf.Sin((timeToEffectEnd / duration) * Mathf.PI) * 2f * intensity;
                float pulse = Mathf.Sin((timeToSecond / 0.75f) * Mathf.PI) * 0.1f;
                m_Vignette.intensity.Override(overAllIntensity + pulse);
                m_Blur.shutterAngle.Override(360f);
            }
            base.Update();
        }
    }
}
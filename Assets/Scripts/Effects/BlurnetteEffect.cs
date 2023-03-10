using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class BlurnetteEffect : BaseEffect
    {
        Blurnette m_Blurnette;
        AudioSource[] m_AudioSources;
        private float timeToSecond = 0.0f;
        
        public override void StopEffect()
        {
            base.StopEffect();
            m_Blurnette.narrowness.Override(0f);
            foreach (var source in m_AudioSources)
            {
                source.Stop();
            }
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
            foreach (var source in m_AudioSources)
            {
                source.Play();
            }
        }

        public override void Start()
        {
            base.Start();
            m_Blurnette = ScriptableObject.CreateInstance<Blurnette>();
            m_AudioSources = GetComponents<AudioSource>();
            m_Blurnette.narrowness.Override(0f);
            m_Blurnette.enabled.Override(true);
            
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Blurnette);
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
                // -(Math.cos(Math.PI * x) - 1) / 2;
                float animationDuration = Math.Min(duration / 2, 1.5f);
                float intens = intensity;

                if ((duration - timeToEffectEnd) < animationDuration) { 
                    intens *= -((float) Math.Cos(Math.PI * (timeToEffectEnd - duration) / animationDuration) - 1) / 2;
                }
                if (timeToEffectEnd < animationDuration) { 
                    intens *= -((float) Math.Cos(Math.PI * timeToEffectEnd / animationDuration) - 1) / 2;
                }

                intens *= 2.0f;

                float pulse = Mathf.Sin((timeToSecond / 0.75f) * Mathf.PI) * 0.1f;
                m_Blurnette.narrowness.Override((intens + pulse) / 2.1f);
            }
            base.Update();
        }
    }
}
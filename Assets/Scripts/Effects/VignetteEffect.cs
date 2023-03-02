using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class VignetteEffect : BaseEffect
    {
        Vignette m_Vignette;
        AudioSource m_AudioSource;
        
        public override void StopEffect()
        {
            base.StopEffect();
            m_Vignette.intensity.Override( 0f);
            m_AudioSource.Stop();
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
            m_Vignette.intensity.Override(0f);
            m_AudioSource.Play();
        }

        public override void Start()
        {
            base.Start();
            m_Vignette = ScriptableObject.CreateInstance<Vignette>();
            m_AudioSource = GetComponent<AudioSource>();
            m_Vignette.enabled.Override(true);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        }

        public override void Update()
        {
            if (effectRunning)
            {
                m_Vignette.intensity.Override(
                    Mathf.Sin((timeToEffectEnd /duration) * Mathf.PI) * intensity);
            }
            base.Update();
        }
    }
}
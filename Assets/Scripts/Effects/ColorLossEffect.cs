using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class ColorLossEffect : BaseEffect
    {
        ColorGrading m_CG;
        
        public override void StopEffect()
        {
            base.StopEffect();
            m_CG.saturation.Override(0f);
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
        }

        public void Start()
        {
            base.Start();
            m_CG = ScriptableObject.CreateInstance<ColorGrading>();
            m_CG.enabled.Override(true);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_CG);
        }
        
        public void Update()
        {
            if (effectRunning)
            {
                m_CG.saturation.Override(
                    (Mathf.Sin((timeToEffectEnd /duration) * Mathf.PI) * -100f)
                );
            }
            base.Update();
        }
    }
}
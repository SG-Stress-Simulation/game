using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class FlashbangEffect : BaseEffect
    {
        ColorGrading m_CG;

        private float contrastStart = 100f;
        private float whiteStart = 1.0f;

        public override void StopEffect()
        {
            base.StopEffect();
            m_CG.contrast.Override(0f);
            m_CG.brightness.Override(0f);
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
        }

        public override void Start()
        {
            base.Start();
            m_CG = ScriptableObject.CreateInstance<ColorGrading>();
            m_CG.gradingMode.Override(GradingMode.LowDefinitionRange);
            m_CG.enabled.Override(true);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 101f, m_CG);
        }
        
        public override void Update()
        {
            if (effectRunning)
            {
                contrastStart -= Time.deltaTime * 30f;

                if (contrastStart > 0.0f)
                {
                    m_CG.contrast.Override(contrastStart);
                    m_CG.brightness.Override(contrastStart);
                }
                else
                {
                    m_CG.contrast.Override(0f);
                    m_CG.brightness.Override(0f);
                }
            }
            base.Update();
        }
    }
}
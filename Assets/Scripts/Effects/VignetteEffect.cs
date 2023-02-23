using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class VignetteEffect : BaseEffect
    {
        Vignette m_Vignette;
        
        public override void StopEffect()
        {
            base.StopEffect();
            m_Vignette.intensity.Override( 0f);
        }

        public override void StartEffect()
        {
            base.StartEffect();
            m_Vignette.intensity.Override(0f);
        }

        public void Start()
        {
            base.Start();
            m_Vignette = ScriptableObject.CreateInstance<Vignette>();
            m_Vignette.enabled.Override(true);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        }

        public void Update()
        {
            if (effectRunning)
            {
                m_Vignette.intensity.Override(
                    Mathf.Sin((timeToEffectEnd /duration) * Mathf.PI));
            }
            base.Update();
        }
    }
}
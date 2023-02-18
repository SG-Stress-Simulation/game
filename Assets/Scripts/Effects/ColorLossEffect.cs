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

        public override void StartEffect()
        {
            base.StartEffect();
            
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
                m_CG.saturation.Override(Mathf.Lerp(m_CG.saturation.value, 1f, Time.deltaTime * 2f));
            }
            base.Update();
        }
    }
}
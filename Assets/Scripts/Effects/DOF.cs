using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class DOF : BaseEffect
    {
        DepthOfField dof;
        private float defaultFocalDistance;
        
        public override void StopEffect()
        {
            base.StopEffect();
            dof.focusDistance.Override(10f);
            dof.aperture.Override(32f);
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
            dof.focusDistance.Override(0.4f);
            dof.aperture.Override(5f);
        }

        public override void Start()
        {
            base.Start();
            dof = ScriptableObject.CreateInstance<DepthOfField>();
            dof.enabled.Override(true);
            dof.focusDistance.Override(10f);
            dof.aperture.Override(32f);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, dof);
        }
    }
}
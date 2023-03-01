using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class DOF : BaseEffect
    {
        DepthOfField dof;
        
        public override void StopEffect()
        {
            base.StopEffect();
            dof.enabled.Override(false);
        }

        public override void StartEffect(bool oneShot = true, float effectDuration = EffectDefaults.EFFECT_DURATION, float effectIntensity = EffectDefaults.EFFECT_INTENSITY)
        {
            base.StartEffect(oneShot, effectDuration, effectIntensity);
            dof.enabled.Override(true);
            dof.aperture.Override(32f);
        }

        public override void Start()
        {
            base.Start();
            dof = ScriptableObject.CreateInstance<DepthOfField>();
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, dof);
        }
        
        public override void Update()
        {
            if (effectRunning)
            {
                dof.focusDistance.Override(
                    Mathf.Sin((timeToEffectEnd /duration) * Mathf.PI) * 32f);
            }
            base.Update();
        }
    }
}
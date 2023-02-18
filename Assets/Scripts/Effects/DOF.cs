using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Zinnia.Action.Effects
{
    public class DOF : BaseEffect
    {
        DepthOfField dof;
        
        public void StopEffect()
        {
            base.StopEffect();
            dof.enabled.Override(false);
        }

        public override void StartEffect()
        {
            base.StartEffect();
        }

        public void Start()
        {
            base.Start();
            dof = ScriptableObject.CreateInstance<DepthOfField>();
            dof.enabled.Override(true);
            dof.aperture.Override(32f);
            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, dof);
        }
        
        public void Update()
        {
            if (effectRunning)
            {
                dof.focusDistance.Override(timeToEffectEnd / 2);
            }
            base.Update();
        }
    }
}
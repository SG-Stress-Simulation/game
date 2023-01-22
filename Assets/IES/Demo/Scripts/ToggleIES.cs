using System.Collections.Generic;
using UnityEngine;

namespace IESLights.Demo
{
    public class ToggleIES : MonoBehaviour
    {
        private Dictionary<Light, Texture> _spotsToCookies = new Dictionary<Light, Texture>();

        void Start()
        {
            var lights = GetComponentsInChildren<Light>();
            foreach(var light in lights)
            {
                _spotsToCookies.Add(light, light.cookie);
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                foreach(var light in _spotsToCookies.Keys)
                {
                    if(light.cookie == null)
                    {
                        light.cookie = _spotsToCookies[light];
                        light.intensity = .7f;
                    }
                    else
                    {
                        light.cookie = null;
                        light.intensity = .4f;
                    }
                }
            }
        }
    }
}
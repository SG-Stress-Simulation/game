using System.Collections.Generic;
using UnityEngine;

namespace IESLights.Demo
{
    public class ToggleCameraPosition : MonoBehaviour
    {
        public List<Transform> Positions;

        private int _positionIndex;

        private void Start()
        {
            transform.position = Positions[_positionIndex].position;
            transform.rotation = Positions[_positionIndex].rotation;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                _positionIndex++;
                _positionIndex %= Positions.Count;
            }

            transform.position = Positions[_positionIndex].position;
            transform.rotation = Positions[_positionIndex].rotation;
        }
    }
}
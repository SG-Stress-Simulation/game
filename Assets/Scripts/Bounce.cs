using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [Tooltip("Y-Axis offset of the object.")]
    public float offset;

    [Tooltip("Divider for cos function to reduce bounce height.")]
    public int hoverDivider;
        
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, MathF.Cos(Time.time) / hoverDivider + offset, transform.position.z);    
    }
}

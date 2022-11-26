using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBelt : MonoBehaviour
{
    public GameObject camera;
    public float cameraOffsetBehind = 0.1f;
    public float cameraOffsetBelow = 0.1f;
    public float torsoOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // It is used to attach the belt to the camera
    void Update()
    {
        // First we need to calculate an anchor point relative to the camera position and rotation
        // The anchor point is located 0.15 meters behind and 0.25 meters below the camera
        Vector3 anchorPoint = camera.transform.position - camera.transform.forward * cameraOffsetBehind - camera.transform.up * cameraOffsetBelow;
        // From this anchor point we calculate the position of the belt
        // It is located 0.3 meters below the anchor point in absolute coordinates
        Vector3 beltPosition = anchorPoint - Vector3.up * torsoOffset;
        // We set the belt position
        transform.position = beltPosition;
        // The belt rotation is the same in the y axis as the camera rotation
        transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
    }
}

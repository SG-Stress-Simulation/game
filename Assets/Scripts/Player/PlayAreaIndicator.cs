using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayAreaIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("PlayAreaIndicator Start");

        List<Vector3> boundaryPoints = new List<Vector3>();

        List<XRInputSubsystem> lst = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(lst);

        bool found = false;
        for (int i = 0; i < lst.Count; i++) {
            bool v = lst[i].TryGetBoundaryPoints(boundaryPoints);
            if (v) {
                found = true;
                // Debug.Log("Boundary found");
                break;
            };
        }

        if (!found) {
            // Debug.Log("No boundary found");
            return;
        }

        // shift the boundary points 0.05m up
        for (int i = 0; i < boundaryPoints.Count; i++) {
            boundaryPoints[i] += new Vector3(0, 0.05f, 0);
        }

        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = boundaryPoints.Count;

        lineRenderer.SetPositions(boundaryPoints.ToArray());

        lineRenderer.loop = true;

        lineRenderer.startWidth = 0.05f;

        lineRenderer.endWidth = 0.05f;

        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));

        lineRenderer.material.color = Color.red;

        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

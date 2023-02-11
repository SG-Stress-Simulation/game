using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayAreaIndicator : MonoBehaviour
{
    private int _counter = 0;

    private void LoadBoundary()
    {
        List<Vector3> boundaryPoints = new List<Vector3>();

        List<XRInputSubsystem> lst = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(lst);

        bool found = false;
        for (int i = 0; i < lst.Count; i++) {
            bool v = lst[i].TryGetBoundaryPoints(boundaryPoints);
            if (v) {
                found = true;
                break;
            };
        }

        if (!found) {
            return;
        }

        for (int i = 0; i < boundaryPoints.Count; i++) {
            boundaryPoints[i] += new Vector3(0, 0.05f, 0);
        }

        _counter = boundaryPoints.Count;

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
    
    // Start is called before the first frame update
    void Start()
    {
        LoadBoundary();
    }

    // Update is called once per frame
    void Update()
    {
        // try to load boundary when it is not loaded (required if game is started before headset is connected)
        if (_counter == 0) {
            LoadBoundary();
        }
    }
}

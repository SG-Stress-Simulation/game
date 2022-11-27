using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineBehaviour : MonoBehaviour
{
    public GameObject sourceLeft;
    public GameObject sourceRight;


    // Start is called before the first frame update
    private void Start()
    {
        // Fetch collider and check if it exists
        Debug.Log("Trigger for " + gameObject.name + " is On: " + GetComponent<Collider>().isTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other == sourceLeft.GetComponent<Collider>() || other == sourceRight.GetComponent<Collider>())
        {
            Debug.Log("Trigger entered");
            var outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == sourceLeft.GetComponent<Collider>() || other == sourceRight.GetComponent<Collider>())
        {
            Destroy(GetComponent<Outline>());
        }
    }
}
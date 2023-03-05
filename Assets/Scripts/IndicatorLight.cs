using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    public Material matOn;
    public Material matOff;
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isOn)
        {
            GetComponent<Renderer>().material = matOn;
        }
        else
        {
            GetComponent<Renderer>().material = matOff;
        }
    }

    public void Toggle()
    {
        isOn = !isOn;
        if (isOn)
        {
            GetComponent<Renderer>().material = matOn;
        }
        else
        {
            GetComponent<Renderer>().material = matOff;
        }
    }

    public void TurnOn()
    {
        isOn = true;
        GetComponent<Renderer>().material = matOn;
    }

    public void TurnOff()
    {
        isOn = false;
        GetComponent<Renderer>().material = matOff;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action.Effects;

public class KBDController : MonoBehaviour
{
    public ColorLossEffect colorLossEffect;
    public VignetteEffect vignetteEffect;
    public DOF dofEffect;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            vignetteEffect.StartEffect();
        } else if(Input.GetKeyUp(KeyCode.X))
        {
            dofEffect.StartEffect();
        } else if (Input.GetKeyUp(KeyCode.C))
        {
            colorLossEffect.StartEffect();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
  public GameObject leftMenuInteractor;
  public GameObject rightMenuInteractor;

  public bool hideButtonOnPress = true;

  public bool disableInteractorOnPress = true;

    public void OnPress()
    {
        // TODO: make this configurable
        // for now only do start action

        Debug.Log("Button Pressed");

        // remove button
        if (hideButtonOnPress)
            gameObject.SetActive(false);

        // disable interactor
        if (disableInteractorOnPress)
        {
            if (leftMenuInteractor != null) leftMenuInteractor.SetActive(false);
            if (rightMenuInteractor != null) rightMenuInteractor.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

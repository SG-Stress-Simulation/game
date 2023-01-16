using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
  [Header("General Options")]
  public bool buttonEnabled = true;
  public bool hideButtonOnPress = true;
  public GameObject leftMenuInteractor;
  public GameObject rightMenuInteractor;
  public bool disableInteractorOnPress = false;
  public GameObject menu;
  public bool disableMenuOnPress = false;

  [Header("Button States")]
  public GameObject normalState;
  public GameObject hoverState;
  public GameObject deactivatedState;

  public void OnPress()
  {
    // remove button
    if (hideButtonOnPress)
      gameObject.SetActive(false);

    // disable interactor
    if (disableInteractorOnPress)
    {
      if (leftMenuInteractor != null) leftMenuInteractor.SetActive(false);
      if (rightMenuInteractor != null) rightMenuInteractor.SetActive(false);
    }

    // disable menu
    if (disableMenuOnPress)
    {
      if (menu != null) menu.SetActive(false);
    }
  }

  public void OnHover(bool hover)
  {
    if (!buttonEnabled) return;

    normalState.SetActive(!hover);
    hoverState.SetActive(hover);
  }

  // Start is called before the first frame update
  protected void Start() { 
    normalState.SetActive(buttonEnabled);
    hoverState.SetActive(false);
    deactivatedState.SetActive(!buttonEnabled);
  }

  // Update is called once per frame
  abstract public void Update();
}

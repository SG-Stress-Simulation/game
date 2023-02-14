using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
  public GameObject leftInteractor;
  public GameObject rightInteractor;

  public GameObject mainMenu;
  //   list of all submenus
  public List<GameObject> submenus = new List<GameObject>();

  // Start is called before the first frame update
  public void Enable() {
    leftInteractor.SetActive(true);
    rightInteractor.SetActive(true);
  }

  public void Disable() {
    gameObject.SetActive(false);
    leftInteractor.SetActive(false);
    rightInteractor.SetActive(false);
    Reset();
  }

  public void Reset() {
    mainMenu.SetActive(true);
    foreach (GameObject submenu in submenus) {
      submenu.SetActive(false);
    }
  }
}

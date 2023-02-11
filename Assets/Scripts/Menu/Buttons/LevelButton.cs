using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : Button
{
  [Header("Level Button Options")]
  public GameObject level;

  new public void OnPress()
  {
    if (!buttonEnabled) return;

    base.OnPress();

    Debug.Log("Level Button Pressed");

    level.SetActive(true);
  }

  // Start is called before the first frame update
  new void Start()
  {
    base.Start();
  }
}

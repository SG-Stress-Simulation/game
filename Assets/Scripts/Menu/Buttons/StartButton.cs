using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : Button
{ 
  [Header("Start Button Options")]
  public GameObject levelMenu;

  new public void OnPress()
  { 
    if (!buttonEnabled) return;

    base.OnPress();

    Debug.Log("Start Button Pressed");
    levelMenu.SetActive(true);
  }

  // Start is called before the first frame update
  new void Start()
  {
    base.Start();
  }

  // Update is called once per frame
  public override void Update()
  {

  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : Button
{
  [Header("Level Scene Reference")]
  public string scene;

  new public void OnPress()
  {
    if (!buttonEnabled) return;

    base.OnPress();

    Debug.Log("Level Button Pressed");

    SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
  }

  // Start is called before the first frame update
  new void Start()
  {
    base.Start();
  }
}

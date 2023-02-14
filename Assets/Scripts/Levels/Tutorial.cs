using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
  public Animator scene;
  public GuidReference menu;

  public void OnEnable()
  {
    Debug.Log("Tutorial Started");
    scene.SetTrigger("Upwards");
  }

  public void EndLevel()
  {
    Debug.Log("Tutorial Ended");
    scene.SetTrigger("Downwards");
    Invoke("levelEndPostAnimationEvent", 3f);
  }

  public void levelEndPostAnimationEvent()
  {
    if (menu != null) {
      SceneManager.UnloadSceneAsync("Tutorial");
      menu.gameObject.SetActive(true);
      menu.gameObject.SendMessage("Enable");
    }
  }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
  public Animator scene;
  public GuidReference menu;

  public GameObject[] steps;

  private int currentStep = 0;

  public void Start()
  {
    for (int i = 1; i < steps.Length; i++) {
      steps[i].SetActive(false);
    }
    steps[0].SetActive(true);
  }

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

  public void Step() { 
    int nextStep = (currentStep + 1) % steps.Length;
    steps[currentStep].SetActive(false);
    steps[nextStep].SetActive(true);
    currentStep = nextStep; 
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

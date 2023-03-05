using Tilia.Interactions.Interactables.Interactors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
  public Animator scene;
  public GuidReference menu;

  public GameObject[] steps;

  public IndicatorLight[] indicators;

  private int currentStep = 0;
  
  public GuidReference leftInteractor;
  public GuidReference rightInteractor;

  public void Start()
  {
    for (int i = 1; i < steps.Length; i++) {
      steps[i].SetActive(false);
      indicators[i].TurnOff();
    }
    steps[0].SetActive(true);
    indicators[0].TurnOn();
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
    indicators[currentStep].TurnOff();
    steps[nextStep].SetActive(true);
    indicators[nextStep].TurnOn();
    currentStep = nextStep; 
  }

  public void levelEndPostAnimationEvent()
  {
    if (menu != null) {
      
      InteractorFacade leftFacade = leftInteractor.gameObject.GetComponent<InteractorFacade>();
      InteractorFacade rightFacade = rightInteractor.gameObject.GetComponent<InteractorFacade>();
      bool ungrabLeft = false;
      for (int i = 0; i < leftFacade.GrabbedObjects.Count; i++) {
        if (leftFacade.GrabbedObjects[i].gameObject.scene.name == "Tutorial")
          ungrabLeft = true;
      }
      if (ungrabLeft)
        rightFacade.Ungrab();

      bool ungrabRight = false;
      for (int i = 0; i < rightFacade.GrabbedObjects.Count; i++) {
        if (rightFacade.GrabbedObjects[i].gameObject.scene.name == "Tutorial")
          ungrabRight = true;
      }
      if (ungrabRight)
        rightFacade.Ungrab();

      SceneManager.UnloadSceneAsync("Tutorial");
      menu.gameObject.SetActive(true);
      menu.gameObject.SendMessage("Enable");
    }
  }
}

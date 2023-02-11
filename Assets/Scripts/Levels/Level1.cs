using UnityEngine;
using UnityEngine.Events;

public class Level1 : MonoBehaviour
{
  [Header("Level End")]
  public UnityEvent levelEnd = new UnityEvent();
  public UnityEvent levelEndPostAnimation = new UnityEvent();
  
  [Header("Level Start")]
  [Tooltip("The event to trigger when the level moved to the center of the play area")]
  public UnityEvent beforeStart = new UnityEvent();
  public UnityEvent onStarted = new UnityEvent();
  public GameObject collisionForcer;
  public GameObject scene;

  public void OnEnable()
  {
    Debug.Log("Level 1 Started");
  }

  public void EndLevel()
  {
    Debug.Log("Level 1 Ended");
    levelEnd.Invoke();
    Invoke("levelEndPostAnimationEvent", 5f);
  }

  public void StartingSequenceComplete()
  {
    collisionForcer.SetActive(false);
    onStarted.Invoke();
  }

  public void MoveScene()
  {
    scene.GetComponent<Animator>().Play("SidewaysAnim", 0);
  }

  public void EnterRoom()
  {
    beforeStart.Invoke();
    collisionForcer.SetActive(true);

    Invoke("MoveScene", 0.2f);
    Invoke("StartingSequenceComplete", 1f);
  }
  
  // Start is called before the first frame update
  void Start()
  {
    
  }

  public void levelEndPostAnimationEvent()
  {
    levelEndPostAnimation.Invoke();
  }

  // Update is called once per frame
  void Update()
  {
    
  }
}

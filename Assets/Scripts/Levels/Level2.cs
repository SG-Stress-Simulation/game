using UnityEngine;
using UnityEngine.Events;

public class Level2 : MonoBehaviour
{
  [Header("Level End")]
  public UnityEvent levelEnd = new UnityEvent();
  public UnityEvent levelEndPostAnimation = new UnityEvent();
  
  [Header("Level Start")]
  [Tooltip("The event to trigger when the level moved to the center of the play area")]
  public Animator scene;

  public void OnEnable()
  {
    Debug.Log("Level 2 Started");
    scene.SetTrigger("Upwards");
  }

  public void EndLevel()
  {
    Debug.Log("Level 2 Ended");
    levelEnd.Invoke();
    Invoke("levelEndPostAnimationEvent", 3f);
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

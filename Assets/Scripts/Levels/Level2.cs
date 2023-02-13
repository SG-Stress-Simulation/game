using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Level2 : MonoBehaviour
{
    [Header("Level End")]
    public UnityEvent levelEnd = new UnityEvent();
    public UnityEvent levelEndPostAnimation = new UnityEvent();
  
    [Header("Level Start")]
    [Tooltip("The event to trigger when the level moved to the center of the play area")]
    public UnityEvent beforeStart = new UnityEvent();
    public UnityEvent onStarted = new UnityEvent();
    public GameObject scene;

    public void OnEnable()
    {
        Debug.Log("Level 2 Started");
    }

    public void EndLevel()
    {
        Debug.Log("Level 2 Ended");
        levelEnd.Invoke();
        Invoke("levelEndPostAnimationEvent", 5f);
    }

    public void StartingSequenceComplete()
    {
        onStarted.Invoke();
    }

    public void EnterRoom()
    {
        beforeStart.Invoke();
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            
        }
    }
}
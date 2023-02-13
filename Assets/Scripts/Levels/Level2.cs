using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Level2 : MonoBehaviour
{
    [Header("Level End")]
    public UnityEvent levelEnd = new UnityEvent();
    public UnityEvent levelEndPostAnimation = new UnityEvent();
  
    [Header("Level Start")]
    [Tooltip("The event to trigger when the level starts")]
    public UnityEvent onStart = new UnityEvent();

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

    public void levelEndPostAnimationEvent()
    {
        levelEndPostAnimation.Invoke();
    }
}
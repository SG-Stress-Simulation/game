using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    [Header("Level Start")]
    [Tooltip("The event to trigger when the level moved to the center of the play area")]
    public UnityEvent onRoomEnter = new UnityEvent();
    public UnityEvent onRoomEntered = new UnityEvent();
    public GuidReference collisionForcer;
    public Animator scene;
    public GuidReference menu;

    public void OnEnable()
    {
        Debug.Log("Level 3 Started");
    }

    public void EndLevel()
    {
        Debug.Log("Level 3 Ended");
        scene.SetTrigger("Downwards");
        Invoke("levelEndPostAnimationEvent", 5f);
    }

    public void StartingSequenceComplete()
    {
        collisionForcer.gameObject.SetActive(false);
        onRoomEntered.Invoke();
    }

    public void MoveScene()
    {
        scene.Play("SidewaysAnim", 0);
    }

    public void EnterRoom()
    {
        onRoomEnter.Invoke();
        collisionForcer.gameObject.SetActive(true);

        Invoke("MoveScene", 0.2f);
        Invoke("StartingSequenceComplete", 1f);
    }

    public void levelEndPostAnimationEvent()
    {
        if (menu != null) {
            SceneManager.UnloadSceneAsync("Level 3");
            menu.gameObject.SetActive(true);
            menu.gameObject.SendMessage("Enable");
        }
    }
}
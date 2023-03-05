using Tilia.Interactions.Interactables.Interactors;
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
    public GuidReference leftInteractor;
    public GuidReference rightInteractor;

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
        if (collisionForcer.gameObject)
            collisionForcer.gameObject.GetComponent<SphereCollider>().enabled = false;
        onRoomEntered.Invoke();
    }

    public void MoveScene()
    {
        scene.Play("SidewaysAnim", 0);
    }

    public void EnterRoom()
    {
        onRoomEnter.Invoke();
        if (collisionForcer.gameObject)
        collisionForcer.gameObject.GetComponent<SphereCollider>().enabled = true;

        Invoke("MoveScene", 0.2f);
        Invoke("StartingSequenceComplete", 1f);
    }

    public void levelEndPostAnimationEvent()
    {
        if (menu != null) {
            InteractorFacade leftFacade = leftInteractor.gameObject.GetComponent<InteractorFacade>();
            InteractorFacade rightFacade = rightInteractor.gameObject.GetComponent<InteractorFacade>();
            bool ungrabLeft = false;
            for (int i = 0; i < leftFacade.GrabbedObjects.Count; i++) {
            if (leftFacade.GrabbedObjects[i].gameObject.scene.name == "Level 1")
                ungrabLeft = true;
            }
            if (ungrabLeft)
            rightFacade.Ungrab();

            bool ungrabRight = false;
            for (int i = 0; i < rightFacade.GrabbedObjects.Count; i++) {
            if (rightFacade.GrabbedObjects[i].gameObject.scene.name == "Level 1")
                ungrabRight = true;
            }
            if (ungrabRight)
            rightFacade.Ungrab();

            SceneManager.UnloadSceneAsync("Level 3");
            menu.gameObject.SetActive(true);
            menu.gameObject.SendMessage("Enable");
        }
    }
}
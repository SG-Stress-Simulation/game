using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public Animator scene;
    public GuidReference menu;

    public void OnEnable()
    {
        Debug.Log("Level 2 Started");
        scene.SetTrigger("Upwards");
    }

    public void EndLevel()
    {
        Debug.Log("Level 2 Ended");
        scene.SetTrigger("Downwards");
        Invoke("levelEndPostAnimationEvent", 5f);
    }

    public void levelEndPostAnimationEvent()
    {
        if (menu != null) {
            SceneManager.UnloadSceneAsync("Level 2");
            menu.gameObject.SetActive(true);
            menu.gameObject.SendMessage("Enable");
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    public Animator scene;
    public GuidReference menu;

    public void OnEnable()
    {
        Debug.Log("Level 3 Started");
        scene.SetTrigger("Upwards");
    }

    public void EndLevel()
    {
        Debug.Log("Level 3 Ended");
        scene.SetTrigger("Downwards");
        Invoke("levelEndPostAnimationEvent", 5f);
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
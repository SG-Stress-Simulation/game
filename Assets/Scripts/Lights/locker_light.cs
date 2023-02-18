using UnityEngine;
using Zinnia.Action;

public class locker_light : MonoBehaviour
{
    private bool lightState;
    private MeshRenderer myRend;
    [Header("Door")]
    public GameObject door;

    [Header("Lights")]
    public GameObject[] lightSources;

    [Header("On State")]
    public AudioClip onSound;
    public Material bulbOnMat;

    [Header("Off State")]
    public AudioClip offSound;
    public Material bulbOffMat;

    // Start is called before the first frame update
    void Start()
    {
        lightState = lightSources[0].GetComponent<Light>().isActiveAndEnabled;
        myRend = this.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        bool activated = (door.transform.localRotation.eulerAngles.y > 45) && (door.transform.localRotation.eulerAngles.y < 200);


        if (activated)
        {
            Debug.Log(door.transform.localRotation.eulerAngles.y);

            if (!lightState)
            {
                GetComponent<AudioSource>().clip = onSound;
                GetComponent<AudioSource>().Play();
                foreach (GameObject x in lightSources)
                {
                    x.GetComponent<Light>().enabled = true;
                }
                lightState = true;
                Material[] materials = myRend.materials;
                materials[0] = bulbOnMat;
                myRend.materials = materials;
            }
        }
        else
        {
            if (lightState)
            {
                GetComponent<AudioSource>().clip = offSound;
                GetComponent<AudioSource>().Play();
                foreach (GameObject x in lightSources)
                {
                    x.GetComponent<Light>().enabled = false;
                }
                lightState = false;
                Material[] materials = myRend.materials;
                materials[0] = bulbOffMat;
                myRend.materials = materials;
            }
        }
    }
}



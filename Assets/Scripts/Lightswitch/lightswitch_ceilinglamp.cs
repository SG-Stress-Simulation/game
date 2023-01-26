using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class lightswitch_ceilinglamp : MonoBehaviour
{
    public BooleanAction triggerPressed;

    public GameObject[] lightsources;
    public GameObject[] lampshades;
    public GameObject[] bulbs;

    private bool lightState;

    public AudioClip onSound;
    public AudioClip offSound;
    private AudioSource audio;

    public Material bulbOnMat;
    public Material bulbOffMat;
    public Material shadeOnMat;
    public Material shadeOffMat;

    public Collider collider;

    public GameObject hand;
    public GameObject button;

    private Vector3 minPosition = new Vector3(0, 0.6f, -1.7f);
    private Vector3 maxPosition = new Vector3(0.3f, 1.3f, -1.32f);

    // Start is called before the first frame update
    void Start()
    {
        print("yoo");
        lightState = false;
        audio = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        //bool trigger = triggerPressed != null ? triggerPressed.Value : false;
        Vector3 handPos = hand.transform.position;

        if (Input.GetKeyDown(KeyCode.L) && collider.bounds.Contains(handPos))
        {
            if (!lightState)
            {
                audio.clip = onSound;
                audio.Play();
                foreach (GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = true;
                }
                for (int i = 0; i < lampshades.Length; i++)
                {
                    lampshades[i].GetComponent<MeshRenderer>().material = shadeOnMat;
                    if (i < 2) bulbs[i].GetComponent<MeshRenderer>().material = bulbOnMat;
                }
                lightState = true;

                button.transform.eulerAngles = new Vector3(
                    button.transform.eulerAngles.x,
                    button.transform.eulerAngles.y,
                    -5
                );
            }
            else
            {
                audio.clip = offSound;
                audio.Play();
                foreach (GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = false;
                }
                for (int i = 0; i < lampshades.Length; i++)
                {
                    lampshades[i].GetComponent<MeshRenderer>().material = shadeOffMat;
                    if (i < 2) bulbs[i].GetComponent<MeshRenderer>().material = bulbOffMat;
                }
                lightState = false;

                button.transform.eulerAngles = new Vector3(
                   button.transform.eulerAngles.x,
                   button.transform.eulerAngles.y,
                   5
               );
            }
        }
    }
}
    

    /*
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L) || switcher)
        {
            if (!lightState)
            {
                audio.clip = onSound;
                audio.Play();
                foreach(GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = true;
                }
                for(int i=0; i < lampshades.Length; i++)
                {
                    lampshades[i].GetComponent<MeshRenderer>().material = shadeOnMat;
                    if(i < 2) bulbs[i].GetComponent<MeshRenderer>().material = bulbOnMat;
                }
                lightState = true;
            } else
            {
                audio.clip = offSound;
                audio.Play();
                foreach (GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = false;
                }
                for (int i = 0; i < lampshades.Length; i++)
                {
                    lampshades[i].GetComponent<MeshRenderer>().material = shadeOffMat;
                    if (i < 2) bulbs[i].GetComponent<MeshRenderer>().material = bulbOffMat;
                }
                lightState = false;
            }
        }
    }*/


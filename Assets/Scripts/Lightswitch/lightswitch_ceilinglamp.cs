using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class lightswitch_ceilinglamp : MonoBehaviour
{
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;

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

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject button;

    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lightState = true;
        audio = this.GetComponent<AudioSource>();
    }

    void Update()
    {

        if (debounceTimer > 0f)
        {
            debounceTimer -= Time.deltaTime;
            return;
        }

        bool leftTrigger = leftTriggerPressed != null ? leftTriggerPressed.Value : false;
        bool rightTrigger = rightTriggerPressed != null ? rightTriggerPressed.Value : false;

        bool isPressedL = leftTrigger && collider.bounds.Contains(leftHand.transform.position);
        bool isPressedR = rightTrigger && collider.bounds.Contains(rightHand.transform.position);
        bool isPressedSim = Input.GetKeyDown(KeyCode.L) && (collider.bounds.Contains(leftHand.transform.position) || collider.bounds.Contains(rightHand.transform.position));

        if (isPressedL || isPressedR || isPressedSim)
        {
            Debug.Log("Ceiling light switch pressed!");

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
            debounceTimer = debounceTime;
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


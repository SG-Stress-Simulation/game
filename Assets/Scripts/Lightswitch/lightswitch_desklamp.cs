using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class lightswitch_desklamp : MonoBehaviour
{
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;

    public GameObject[] lightsources;

    private bool lightState;

    public AudioClip onSound;
    public AudioClip offSound;
    private AudioSource audio;

    public Material bulbOnMat;
    public Material bulbOffMat;
    public MeshRenderer myRend;

    public Collider collider;

    public GameObject leftHand;
    public GameObject rightHand;

    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lightState = lightsources[0].GetComponent<Light>().isActiveAndEnabled;
        audio = this.GetComponent<AudioSource>();
        myRend = this.GetComponent<MeshRenderer>();
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
            Debug.Log("Desk light switch pressed!");

            if (!lightState)
            {
                audio.clip = onSound;
                audio.Play();
                foreach (GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = true;
                }
                lightState = true;
                Material[] materials = myRend.materials;
                materials[5] = bulbOnMat;
                myRend.materials = materials;
            }
            else
            {
                audio.clip = offSound;
                audio.Play();
                foreach (GameObject x in lightsources)
                {
                    x.GetComponent<Light>().enabled = false;
                }
                lightState = false;
                Material[] materials = myRend.materials;
                materials[5] = bulbOffMat;
                myRend.materials = materials;
            }
            debounceTimer = debounceTime;
        }
    }
}
   


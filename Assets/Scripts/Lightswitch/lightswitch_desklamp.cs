using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class lightswitch_desklamp : MonoBehaviour
{
    public BooleanAction triggerPressed;

    public GameObject[] lightsources;

    private bool lightState;

    public AudioClip onSound;
    public AudioClip offSound;
    private AudioSource audio;

    public Material bulbOnMat;
    public Material bulbOffMat;
    public MeshRenderer myRend;

    public Collider collider;

    public GameObject hand;


    // Start is called before the first frame update
    void Start()
    {
        lightState = lightsources[0].GetComponent<Light>().isActiveAndEnabled;
        audio = this.GetComponent<AudioSource>();
        myRend = this.GetComponent<MeshRenderer>();
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
        }
    }
}
   


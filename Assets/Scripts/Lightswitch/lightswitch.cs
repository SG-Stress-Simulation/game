using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightswitch : MonoBehaviour
{

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



    // Start is called before the first frame update
    void Start()
    {
        lightState = false;
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
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
    }
}

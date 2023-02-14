using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class lightswitch_ceilinglamp : MonoBehaviour
{
    private bool lightState;

    [Header("Interactors")]
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;
    public GuidReference leftHand;
    public GuidReference rightHand;

    [Header("Lights")]
    public GameObject[] lightSources;
    public GameObject[] lampshades;
    public GameObject[] bulbs;

    [Header("On State")]
    public AudioClip onSound;
    public Material bulbOnMat;
    public Material shadeOnMat;

    [Header("Off State")]
    public AudioClip offSound;
    public Material bulbOffMat;
    public Material shadeOffMat;

    [Header("Button")]
    public GameObject button;

    [Header("Debounce")]
    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lightState = lightSources[0].GetComponent<Light>().isActiveAndEnabled;
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

        bool isPressedL = leftTrigger && GetComponent<Collider>().bounds.Contains(leftHand.gameObject.transform.position);
        bool isPressedR = rightTrigger && GetComponent<Collider>().bounds.Contains(rightHand.gameObject.transform.position);
        bool isPressedSim = Input.GetKeyDown(KeyCode.L) && (GetComponent<Collider>().bounds.Contains(leftHand.gameObject.transform.position) || GetComponent<Collider>().bounds.Contains(rightHand.gameObject.transform.position));

        if (isPressedL || isPressedR || isPressedSim)
        {
            Debug.Log("Ceiling light switch pressed!");

            if (!lightState)
            {
                GetComponent<AudioSource>().clip = onSound;
                GetComponent<AudioSource>().Play();
                foreach (GameObject x in lightSources)
                {
                    x.GetComponent<Light>().enabled = true;
                }
                for (int i = 0; i < lampshades.Length; i++)
                {
                    lampshades[i].GetComponent<MeshRenderer>().material = shadeOnMat;
                    
                }
                foreach (GameObject x in bulbs)
                {
                    x.GetComponent<MeshRenderer>().material = bulbOnMat;
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
                GetComponent<AudioSource>().clip = offSound;
                GetComponent<AudioSource>().Play();
                foreach (GameObject x in lightSources)
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


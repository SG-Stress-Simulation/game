using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class radio : MonoBehaviour
{
    private bool state;

    [Header("Interactors")]
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;
    public GuidReference leftHand;
    public GuidReference rightHand;

    [Header("Radio Noise")]
    public AudioClip radioNoise;

    [Header("Debounce")]
    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    [Header("Button")]
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
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
            if(state)
            {
                GetComponent<AudioSource>().Stop();
                state= false;

                button.transform.eulerAngles = new Vector3(
                    30,
                    button.transform.eulerAngles.y,
                    button.transform.eulerAngles.z
                );
            } else
            {
                GetComponent<AudioSource>().clip = radioNoise;
                GetComponent<AudioSource>().Play();
                state = true;

                button.transform.eulerAngles = new Vector3(
                    -30,
                    button.transform.eulerAngles.y,
                    button.transform.eulerAngles.z
                );
            }
            debounceTimer = debounceTime;
        }
    }
}

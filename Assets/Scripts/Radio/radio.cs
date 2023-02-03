using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class radio : MonoBehaviour
{
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;

    private bool state;

    public AudioClip radioNoise;
    private AudioSource audio;

    public GameObject leftHand;
    public GameObject rightHand;

    public Collider collider;

    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
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

        bool isPressedL = leftTrigger && collider.bounds.Contains(leftHand.transform.position);
        bool isPressedR = rightTrigger && collider.bounds.Contains(rightHand.transform.position);
        bool isPressedSim = Input.GetKeyDown(KeyCode.L) && (collider.bounds.Contains(leftHand.transform.position) || collider.bounds.Contains(rightHand.transform.position));

        if (isPressedL || isPressedR || isPressedSim)
        {
            if(state)
            {
                audio.Stop();
                state= false;

                button.transform.eulerAngles = new Vector3(
                    30,
                    button.transform.eulerAngles.y,
                    button.transform.eulerAngles.z
                );
            } else
            {
                audio.clip = radioNoise;
                audio.Play();
                state = true;

                button.transform.eulerAngles = new Vector3(
                    -30,
                    button.transform.eulerAngles.y,
                    button.transform.eulerAngles.z
                );
            }

        }
    }
}

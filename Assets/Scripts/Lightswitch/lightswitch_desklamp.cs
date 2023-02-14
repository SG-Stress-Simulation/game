using UnityEngine;
using Zinnia.Action;

public class lightswitch_desklamp : MonoBehaviour
{
    private bool lightState;
    private MeshRenderer myRend;

    [Header("Interactors")]
    public BooleanAction leftTriggerPressed;
    public BooleanAction rightTriggerPressed;
    public GuidReference leftHand;
    public GuidReference rightHand;

    [Header("Lights")]
    public GameObject[] lightSources;

    [Header("On State")]
    public AudioClip onSound;
    public Material bulbOnMat;

    [Header("Off State")]
    public AudioClip offSound;
    public Material bulbOffMat;
    
    [Header("Debounce")]
    public float debounceTime = 0.5f;
    private float debounceTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lightState = lightSources[0].GetComponent<Light>().isActiveAndEnabled;
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

        bool isPressedL = leftTrigger && GetComponent<Collider>().bounds.Contains(leftHand.gameObject.transform.position);
        bool isPressedR = rightTrigger && GetComponent<Collider>().bounds.Contains(rightHand.gameObject.transform.position);
        bool isPressedSim = Input.GetKeyDown(KeyCode.L) && (GetComponent<Collider>().bounds.Contains(leftHand.gameObject.transform.position) || GetComponent<Collider>().bounds.Contains(rightHand.gameObject.transform.position));

        if (isPressedL || isPressedR || isPressedSim)
        {
            Debug.Log("Desk light switch pressed!");

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
                materials[5] = bulbOnMat;
                myRend.materials = materials;
            }
            else
            {
                GetComponent<AudioSource>().clip = offSound;
                GetComponent<AudioSource>().Play();
                foreach (GameObject x in lightSources)
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
   


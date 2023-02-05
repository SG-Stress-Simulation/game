using UnityEngine;
using UnityEngine.Events;

public class MoveTarget : MonoBehaviour
{

  public GameObject player;
  public GameObject target;
  public UnityEvent onSuccess = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if player in target hitbox
        if (player != null && target != null)
        {
          if (target.GetComponent<Collider>().bounds.Contains(player.transform.position))
          {
            // trigger success event
            onSuccess.Invoke();
          }
        }
    }
}
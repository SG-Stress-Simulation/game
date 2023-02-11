using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeAfterTime : MonoBehaviour
{
    public float removeTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        removeTimer -= Time.deltaTime;
        if (removeTimer < 0) {
            Destroy(gameObject);
        }
    }
}

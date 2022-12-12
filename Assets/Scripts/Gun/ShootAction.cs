using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    // Update is called once per frame
    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(gameObject. transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
            }
        }
    }
}

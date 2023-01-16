using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class MenuInteractor : MonoBehaviour
{

  public LayerMask layerMask;

  // boolean input actions
  public BooleanAction triggerPressed;
  public float cutoffTime = 0.5f;

  public BooleanAction gripPressed;
  public FloatAction triggerAxis;

  LineRenderer rend;
  Vector3[] points;
  GameObject hovered;

  float lastActiveTime;

  void Start()
  {
    rend = gameObject.GetComponent<LineRenderer>();

    points = new Vector3[2];

    points[0] = Vector3.zero;

    points[1] = Vector3.forward * 10;

    rend.SetPositions(points);
    rend.material = new Material(Shader.Find("Unlit/Color"));
    rend.enabled = true;
  }

  public void ButtonHover(GameObject comp)
  {
    if (hovered != null)
    {
      hovered.SendMessage("OnHover", false);
    }
    
    if (comp == null)
      return;

    hovered = comp;

    comp.SendMessage("OnHover", true);
  }

  public void ButtonPress(GameObject comp)
  {
    if (comp == null)
      return;

    comp.SendMessage("OnPress");
  }

  public void AlignLineRenderer(LineRenderer rend)
  {
    Ray ray;
    ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    points[0] = ray.origin;

    if (Physics.Raycast(ray, out hit, 100, layerMask.value))
    {
      points[1] = hit.point;
      rend.material.color = Color.green;

      GameObject comp = hit.collider.gameObject;

      bool trigger = triggerPressed != null ? triggerPressed.Value : false;

      if (trigger)
      { 
        ButtonPress(comp);
      } 
      else
      {
        ButtonHover(comp);
      }
    }
    else
    {
      points[1] = ray.GetPoint(20);
      rend.material.color = Color.red;

      ButtonHover(null);
    }

    rend.SetPositions(points);
  }

  // Update is called once per frame
  void Update()
  {
    bool grip = gripPressed != null ? gripPressed.Value : true;
    bool trigger = triggerAxis != null ? triggerAxis.Value > 0.2f : false;

    // lastActiveTime more than 0.5 seconds ago
    bool inactive = (Time.time - lastActiveTime) > cutoffTime;

    if (grip && !trigger)
    {
      lastActiveTime = Time.time;
    }

    if (inactive && (!grip || trigger))
    {
      rend.enabled = false;
    }
    else
    {
      rend.enabled = true;
      AlignLineRenderer(rend);
    }

  }
}

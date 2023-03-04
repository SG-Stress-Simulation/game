using UnityEngine;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
  [Header("General Options")]
  public UnityEvent onToggle = new UnityEvent();
  public UnityEvent onToggleOff = new UnityEvent();
  public UnityEvent onToggleOn = new UnityEvent();

  public bool initialIsToggled = false;

  [Header("Button States")]
  public GameObject normalStateOn;
  public GameObject hoverStateOn;
  public GameObject normalStateOff;
  public GameObject hoverStateOff;

  private bool isToggled = false;

  protected virtual void ToggleOn()
  {
    isToggled = true;
    normalStateOff.SetActive(false);
    hoverStateOff.SetActive(false);
    normalStateOn.SetActive(true);
    hoverStateOn.SetActive(false);
  }

  protected virtual void ToggleOff()
  {
    isToggled = false;
    normalStateOff.SetActive(true);
    hoverStateOff.SetActive(false);
    normalStateOn.SetActive(false);
    hoverStateOn.SetActive(false);
  }

  public void OnPress()
  {
    onToggle.Invoke();
    if (isToggled)
    {
      onToggleOff.Invoke();
      ToggleOff();
    }
    else
    {
      onToggleOn.Invoke();
      ToggleOn();
    }
  }

  public void OnHover(bool hover)
  {
    if (isToggled)
    {
      normalStateOn.SetActive(!hover);
      hoverStateOn.SetActive(hover);
    }
    else
    {
      normalStateOff.SetActive(!hover);
      hoverStateOff.SetActive(hover);
    }
  }

  // Start is called before the first frame update
  protected void Start() { 
    isToggled = initialIsToggled;
    normalStateOff.SetActive(!initialIsToggled);
    hoverStateOff.SetActive(false);
    normalStateOn.SetActive(initialIsToggled);
    hoverStateOn.SetActive(false);
  }
}

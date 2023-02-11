using UnityEngine;
using UnityEngine.Events;

public abstract class ToggleButton : MonoBehaviour
{
  [Header("General Options")]
  public UnityEvent onToggle1 = new UnityEvent();
  public UnityEvent onToggle2 = new UnityEvent();

  public bool initialIsToggled = false;

  [Header("Button States")]
  public GameObject normalState1;
  public GameObject hoverState1;
  public GameObject normalState2;
  public GameObject hoverState2;

  private bool isToggled = false;

  public void OnPress()
  {
    if (isToggled)
    {
      onToggle1.Invoke();
    }
    else
    {
      onToggle2.Invoke();
    }

    isToggled = !isToggled;
  }

  public void OnHover(bool hover)
  {
    if (isToggled)
    {
      normalState1.SetActive(!hover);
      hoverState1.SetActive(hover);
    }
    else
    {
      normalState2.SetActive(!hover);
      hoverState2.SetActive(hover);
    }
  }

  // Start is called before the first frame update
  protected void Start() { 
    normalState1.SetActive(!initialIsToggled);
    hoverState1.SetActive(false);
    normalState2.SetActive(initialIsToggled);
    hoverState2.SetActive(false);
  }

  // Update is called once per frame
  abstract public void Update();
}

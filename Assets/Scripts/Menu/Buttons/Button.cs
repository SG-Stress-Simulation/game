using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour 
{
  [Header("General Options")]
  public bool buttonEnabled = true;
  public UnityEvent onPress = new UnityEvent();

  [Header("Button States")]
  public GameObject normalState;
  public GameObject hoverState;
  public GameObject deactivatedState;

  public void OnPress()
  {
    if (!buttonEnabled) return;

    onPress.Invoke();
    OnHover(false);
  }

  public void OnHover(bool hover)
  {
    if (!buttonEnabled) return;

    normalState.SetActive(!hover);
    hoverState.SetActive(hover);
  }

  // Start is called before the first frame update
  protected void Start() { 
    normalState.SetActive(buttonEnabled);
    hoverState.SetActive(false);
    deactivatedState.SetActive(!buttonEnabled);
  }
}

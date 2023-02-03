using UnityEngine;
using UnityEngine.Events;

public abstract class Button : MonoBehaviour
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

  // Update is called once per frame
  abstract public void Update();
}

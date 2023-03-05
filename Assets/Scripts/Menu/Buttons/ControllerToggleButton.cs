using UnityEngine;
using UnityEngine.Events;

public class ControllerToggleButton : ToggleButton
{
  [Header("Controller Options")]
  public GameObject LeftInteractor;
  public GameObject RightInteractor;

  public float KnucklesRotation = 0f;
  public float ViveRotation = 30f;

  protected override void ToggleOn()
  {
    base.ToggleOn();
    LeftInteractor.transform.localRotation = Quaternion.Euler(KnucklesRotation, 0, 0);
    RightInteractor.transform.localRotation = Quaternion.Euler(KnucklesRotation, 0, 0);
  }

  protected override void ToggleOff()
  {
    base.ToggleOff();
    LeftInteractor.transform.localRotation = Quaternion.Euler(ViveRotation, 0, 0);
    RightInteractor.transform.localRotation = Quaternion.Euler(ViveRotation, 0, 0);
  }
}

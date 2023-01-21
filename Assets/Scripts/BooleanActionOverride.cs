namespace Zinnia.Action
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class BooleanActionOverride : BooleanAction
  {
    private bool overrideActive;
    private bool lastRealValue = false;

    public void OverrideValue(bool value)
    {
      base.Receive(value);
      overrideActive = true;
    }

    public void OverrideValueOff()
    {
      base.Receive(lastRealValue);
      overrideActive = false;
    }

    public override void Receive(bool value)
    {
      lastRealValue = value;
      if (overrideActive)
        return;
      base.Receive(value);
    }
  }
}
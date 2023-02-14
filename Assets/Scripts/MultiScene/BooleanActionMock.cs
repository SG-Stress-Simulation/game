using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class BooleanActionMock : BooleanAction
{
    public GuidReference[] externalSources;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        foreach (GuidReference externalSource in externalSources) {
            AddSource(externalSource.gameObject.GetComponent<BooleanAction>());
        }
    }
}

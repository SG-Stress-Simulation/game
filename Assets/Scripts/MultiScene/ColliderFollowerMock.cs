using System.Collections;
using System.Collections.Generic;
using Tilia.Trackers.ColliderFollower;
using UnityEngine;

public class ColliderFollowerMock : ColliderFollowerFacade
{
    public GuidReference sourceMock;
    // Start is called before the first frame update
    void Start()
    {
        Source = sourceMock.gameObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Data.Collection.List;

public class UnityObservableListMock : UnityObjectObservableList
{
    public GuidReference[] listMock;
    // Start is called before the first frame update
    private new void Start()
    {
        foreach (var item in listMock)
        {
            Elements.Add(item.gameObject);
        }
        base.Start();
    }
}

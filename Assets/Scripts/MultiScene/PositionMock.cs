using UnityEngine;

public class PositionMock : MonoBehaviour
{
  public GuidReference mock;

    // Update is called once per frame
    void Update()
    {
        if (mock != null)
            gameObject.transform.position = mock.gameObject.transform.position;
    }
}

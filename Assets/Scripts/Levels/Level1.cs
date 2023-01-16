using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
  public float animationDistance = 3f;
  public float animationDuration = 2f;

  private IEnumerator AnimateLevelStart()
  {
    float deltaPos = animationDistance / animationDuration;
    float time = 0f;
    while (time < animationDuration)
    {
      transform.position += new Vector3(0f, deltaPos * Time.deltaTime, 0f);
      time += Time.deltaTime;
      yield return null;
    }
  }

  public void StartLevel()
  {
    Debug.Log("Level 1 Started");
    // animate moving game object up
    StartCoroutine(AnimateLevelStart());
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}

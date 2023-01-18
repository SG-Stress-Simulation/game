using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class Level1 : MonoBehaviour
{
  public float animationDistance = 3f;
  public float animationDuration = 2f;
  public float timeToNextEffect = 60f;
  public float timeToEffectEnd = 10f;

   PostProcessVolume m_Volume;
   Vignette m_Vignette;
  
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
      // Create an instance of a vignette
       m_Vignette = ScriptableObject.CreateInstance<Vignette>();
       m_Vignette.enabled.Override(true);
       m_Vignette.intensity.Override(1f);
      // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
       m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
  }

  // Update is called once per frame
  void Update()
  {
    m_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
  }

  void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}

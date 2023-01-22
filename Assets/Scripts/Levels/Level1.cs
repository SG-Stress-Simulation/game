using System;
using System.Collections;
using System.IO;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level1 : MonoBehaviour
{
  public float animationDistance = 3f;
  public float animationDuration = 2f;
  float timeToNextEffect = 2f;
  float timeToEffectEnd = Mathf.PI * 5;
  bool effectStarted = false;

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
  
  void UpdateEffect()
  {
    m_Vignette.intensity.Override( Mathf.Sin(timeToEffectEnd / 5) * 0.5f + (Mathf.Sin(Time.realtimeSinceStartup)) * 0.2f);
  }
  
  void DisableEffect()
  {
    m_Vignette.intensity.Override( 0f);
    timeToNextEffect = Random.Range(2f, 2f);
    timeToEffectEnd = Mathf.PI * 5;
  }
  
  void StartEffect()
  {
    effectStarted = true;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeToNextEffect = Random.Range(2f, 2f);
    timeToEffectEnd = Mathf.PI * 5;
      // Create an instance of a vignette
       m_Vignette = ScriptableObject.CreateInstance<Vignette>();
       m_Vignette.enabled.Override(true);
      // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
       m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
  }

  // Update is called once per frame
  void Update()
  {
    if(timeToNextEffect > 0f)  {
      timeToNextEffect -= Time.deltaTime;
    } else if(timeToNextEffect <= 0f && !effectStarted) {
      StartEffect();
      Debug.Log("Effect Started");
    } else if(timeToEffectEnd <= 0f && effectStarted) {
      DisableEffect();
      Debug.Log("Effect Disabled");
    } else {
      UpdateEffect();
      timeToEffectEnd -= Time.deltaTime;
      Debug.Log("Time to effect end: " + timeToEffectEnd);
    }
  }

  void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}

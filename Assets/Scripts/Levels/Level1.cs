using System;
using System.Collections;
using System.IO;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Random = UnityEngine.Random;

enum EffectType
{
  FOVDecrease,
  ColorLoss,
}

public class Level1 : MonoBehaviour
{
  public float animationDistance = 3f;
  public float animationDuration = 2f;
  float timeToNextEffect = Random.Range(5f, 10f);
  float timeToEffectEnd = Mathf.PI * 5;
  bool effectStarted = false;
  private EffectType currentEffect = EffectType.FOVDecrease;
  
   PostProcessVolume m_Volume;
   Vignette m_Vignette;
   ColorGrading m_ColorGrading;
  
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
    switch (currentEffect)
    {
      case EffectType.FOVDecrease:
        m_Vignette.intensity.Override( Mathf.Sin(timeToEffectEnd / 5) * 0.5f + (Mathf.Sin(Time.realtimeSinceStartup)) * 0.2f);
        break;
      case EffectType.ColorLoss:
        m_ColorGrading.saturation.Override(Mathf.Sin(timeToEffectEnd / 5));
        break;
    }
  }
  
  void DisableEffect()
  {
    switch (currentEffect)
    {
      case EffectType.FOVDecrease:
        m_Vignette.intensity.Override( 0f);
        break;
      case EffectType.ColorLoss:
        m_ColorGrading.saturation.Override( 0f);
        break;
    }
    timeToNextEffect = Random.Range(5f, 10f);
    timeToEffectEnd = Mathf.PI * 5;
  }
  
  void StartEffect()
  {
    effectStarted = true;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeToNextEffect = Random.Range(5f, 10f);
    timeToEffectEnd = Mathf.PI * 5;
      // Create an instance of a vignette
       m_Vignette = ScriptableObject.CreateInstance<Vignette>();
       m_Vignette.enabled.Override(true);
       
// Create an instance of a color grading
        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        
        // Create a volume and add the post processing effect to it
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette, m_ColorGrading);
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

using System;
using System.Collections;
using System.IO;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Events;
using Tilia.Interactions.Interactables.Interactables;
using Random = UnityEngine.Random;

enum EffectType
{
  FOVDecrease,
  ColorLoss,
}

public class Level1 : MonoBehaviour
{
  private float timeToNextEffect;
  private float timeToEffectEnd;
  private bool effectStarted;

  [Header("Level End")]
  public GameObject levelEndObject;

  public GameObject outsideCollider;

  public UnityEvent levelEnd = new UnityEvent();
  
  [Header("Level Start")]
  public UnityEvent levelStart = new UnityEvent();

  private EffectType currentEffect = EffectType.FOVDecrease;
  
  PostProcessVolume m_Volume;
  Vignette m_Vignette;
  ColorGrading m_ColorGrading;

  public void StartLevel()
  {
    Debug.Log("Level 1 Started");
    levelStart.Invoke();
  }

  public void EndLevel()
  {
    Debug.Log("Level 1 Ended");
    levelEnd.Invoke();
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
  
  // Start is called before the first frame update
  void Start()
  {
    timeToNextEffect = Random.Range(5f, 10f);
    timeToEffectEnd = Mathf.PI * 5;
    effectStarted = false;
    
    // if end object is in outside collider, end level
    if (levelEndObject != null && outsideCollider != null)
    {
      // levelEndObject may not be grabbed currently
      levelEndObject.GetComponent<InteractableFacade>().Ungrabbed.AddListener((_) =>
      {
        if (outsideCollider.GetComponent<Collider>().bounds.Contains(levelEndObject.transform.position))
        {
          EndLevel();
        }
      });
    }

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
      //Debug.Log("Effect Started");
    } else if(timeToEffectEnd <= 0f && effectStarted) {
      DisableEffect();
      //Debug.Log("Effect Disabled");
    } else {
      UpdateEffect();
      timeToEffectEnd -= Time.deltaTime;
      //Debug.Log("Time to effect end: " + timeToEffectEnd);
    }
  }

  void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}

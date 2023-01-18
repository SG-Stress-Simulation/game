using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class Level1 : MonoBehaviour
{
  public float animationDistance = 3f;
  public float animationDuration = 2f;
  public float timeToNextEffect = 520f;
  public float timeToEffectEnd = 520f;

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
    m_Vignette.intensity.value = Mathf.Lerp(0.5f, 1f, (timeToEffectEnd - timeToNextEffect) / timeToEffectEnd);
  }
  
  void DisableEffect()
  {
    m_Vignette.intensity.value = 0f;
    timeToNextEffect = Random.Range(520f, 520f);
    timeToEffectEnd = Random.Range(520f, 520f);
  }
  
  void StartEffect()
  {
    m_Vignette.intensity.value = 0.5f;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeToNextEffect = Random.Range(520f, 520f);
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
    if(timeToNextEffect > 0f)  {
      timeToNextEffect -= Time.deltaTime;
    } else if(timeToNextEffect <= 0f && timeToEffectEnd > 0f) {
      StartEffect();
    } else if(timeToEffectEnd <= 0f) {
      DisableEffect();
    } else {
      UpdateEffect();
    }
  }

  void OnDestroy()
  {
       RuntimeUtilities.DestroyVolume(m_Volume, true, true);
  }
}

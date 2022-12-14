using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshPro))]
public class StartupBehaviour : MonoBehaviour
{
    [Tooltip("Audio to play")]
    public AudioSource audio;

    [Tooltip("in seconds between each alpha-channel change")]
    public float waitTime;

    private bool played;
    private TextMeshPro textMesh;


    private void Start()
    {
        played = false;
        textMesh = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (!Input.GetKeyDown("return") || audio.isPlaying || played) 
            return;
        audio.Play();
        played = true;
        // Ensure fade happens over multiple frames
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() 
    {
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            textMesh.alpha = alpha;
            yield return new WaitForSeconds(waitTime);
        }

        textMesh.alpha = 0f;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class DayNightToggleButton : ToggleButton
{
    [Header("Arena Triangle Object")]
    public GameObject triangles;

    [Header("Arena Tringle Materials")]
    public Material dayMaterial;
    public Material nightMaterial;

    [Header("Arena Lightsources")]
    public GameObject[] lightSources;

    protected override void ToggleOn()
    {
        base.ToggleOn();
        Material[] materials = triangles.GetComponent<MeshRenderer>().materials;
        materials[0] = dayMaterial;
        triangles.GetComponent<MeshRenderer>().materials = materials;

        foreach (GameObject x in lightSources)
        {
            x.GetComponent<Light>().intensity = 1.0f;
        }

    }

    protected override void ToggleOff()
    {
        base.ToggleOff();
        Material[] materials = triangles.GetComponent<MeshRenderer>().materials;
        materials[0] = nightMaterial;
        triangles.GetComponent<MeshRenderer>().materials = materials;

        foreach (GameObject x in lightSources)
        {
            x.GetComponent<Light>().intensity = 0.1f;
        }
    }
}

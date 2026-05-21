using System;
using UnityEngine;
using UnityEngine.UI;
public class OutlineController : MonoBehaviour
{
    public Material outlineMaterial;

    private Renderer[] renderers;
    private Material[][] originalMats;

    public GameObject Frame;

    public float outlineTime = 5f;
    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalMats = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMats[i] = renderers[i].materials;
        }
    }
    public void ShowOutline()
    {
        CancelInvoke(nameof(HideOutline));
        Invoke(nameof(HideOutline), outlineTime);
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] mats = originalMats[i];

            Material[] newMats = new Material[mats.Length + 1];
            for (int j = 0; j < mats.Length; j++)
                newMats[j] = mats[j];

            newMats[mats.Length] = outlineMaterial; 

            renderers[i].materials = newMats;
        }
        Frame.SetActive(true);
    }
    public void HideOutline()
    {

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMats[i];
        }
        Frame.SetActive(false);
    }

}

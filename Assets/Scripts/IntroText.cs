using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    public List<TMP_Text> InfoList;
    [TextArea] public string fullText;
    public float displayTime = 1;
    public RectTransform panelImage;
    public RectTransform Background;
    void Start()
    {
        StartCoroutine(SpawnLines());
    }

    IEnumerator AnimateImageHeight(float from, float to)
    {
        float t = 0f;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            float lerp = Mathf.SmoothStep(0, 1, t / 0.2f);
            float h = Mathf.Lerp(from, to, lerp);

            panelImage.sizeDelta = new Vector2(panelImage.sizeDelta.x, h);
            yield return null;
        }

        panelImage.sizeDelta = new Vector2(panelImage.sizeDelta.x, to);
    }
    IEnumerator SpawnLines()
    {
        yield return StartCoroutine(AnimateImageHeight(0, 83));
        string[] lines = fullText.Split('\n');
        for(int i=0; i<lines.Length; i++)
        {
            StartCoroutine(TypeLine(InfoList[i], lines[i]));
        }
        yield return new WaitForSeconds(displayTime + 2.5f);
        yield return StartCoroutine(AnimateImageHeight(83, 0));
    }
    IEnumerator TypeLine(TMP_Text textMesh, string content)
    {
        textMesh.text = "";
        foreach (char c in content)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(displayTime / content.Length);
        }

        yield return new WaitForSeconds(2);
        for (int i = 0; i < 3; i++)
        {
            textMesh.enabled = false;
            yield return new WaitForSeconds(0.05f);

            textMesh.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        textMesh.enabled = false;
    }
}

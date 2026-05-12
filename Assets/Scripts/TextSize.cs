using TMPro;
using UnityEngine;
[ExecuteInEditMode]
public class TextSize : MonoBehaviour
{
    TMP_Text text;
    RectTransform rectTransform;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if(text.preferredWidth>0)
        rectTransform.sizeDelta = new Vector2(text.preferredWidth+10, text.preferredHeight+10);
        else        rectTransform.sizeDelta = new Vector2(0, 0);
    }
}

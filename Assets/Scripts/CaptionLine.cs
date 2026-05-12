using TMPro;
using UnityEngine;

public class CaptionLine : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform rectTransform;
    float timer = 0;
    void Start()
    {
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }
    void Update()
    {
        timer += Time.deltaTime;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, text.preferredHeight);
        if (timer > 5) 
        {
            Destroy(gameObject);
        }
    }
}

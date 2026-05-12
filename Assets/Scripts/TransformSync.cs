using UnityEngine;

[ExecuteInEditMode]
public class TransformSync : MonoBehaviour
{
    RectTransform rectTransform;
    RectTransform childRectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        childRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    void Update()
    {
        rectTransform.sizeDelta = childRectTransform.sizeDelta;
    }
}

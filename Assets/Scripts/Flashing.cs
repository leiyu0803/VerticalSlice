using TMPro;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    TMP_Text text;
    public bool IsFlashing = false;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (IsFlashing)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, Mathf.PingPong(Time.time * 1, 1)));
        }
    }
}

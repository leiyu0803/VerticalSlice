using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    OutlineController target;                 
    public RectTransform frame;              
    public TextMeshProUGUI hpText;           
    public RectTransform line;               
    EnemyHealth health;
    public CapsuleCollider skinnedMeshRenderer;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        target = GetComponent<OutlineController>();
        health = GetComponent<EnemyHealth>();
    }
    void LateUpdate()
    {
        if (target == null) return;

        Bounds b = skinnedMeshRenderer.bounds;

        Vector3[] corners = new Vector3[8];
        corners[0] = new Vector3(b.min.x, b.min.y, b.min.z);
        corners[1] = new Vector3(b.min.x, b.min.y, b.max.z);
        corners[2] = new Vector3(b.min.x, b.max.y, b.min.z);
        corners[3] = new Vector3(b.min.x, b.max.y, b.max.z);
        corners[4] = new Vector3(b.max.x, b.min.y, b.min.z);
        corners[5] = new Vector3(b.max.x, b.min.y, b.max.z);
        corners[6] = new Vector3(b.max.x, b.max.y, b.min.z);
        corners[7] = new Vector3(b.max.x, b.max.y, b.max.z);

        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);

        bool anyVisible = false;

        for (int i = 0; i < 8; i++)
        {
            Vector3 sp = cam.WorldToScreenPoint(corners[i]);

            if (sp.z > 0)
                anyVisible = true;

            min.x = Mathf.Min(min.x, sp.x);
            min.y = Mathf.Min(min.y, sp.y);
            max.x = Mathf.Max(max.x, sp.x);
            max.y = Mathf.Max(max.y, sp.y);
        }

        if (!anyVisible)
        {
            frame.gameObject.SetActive(false);
            return;
        }

        frame.gameObject.SetActive(true);

        Vector2 size = max - min;
        Vector2 pos = (min + max) * 0.5f;

        frame.position = pos;
        frame.sizeDelta = size;

        hpText.rectTransform.position = new Vector2(min.x, max.y);
        hpText.text = "Health:"+health.health.ToString();

        Vector2 topCenter = new Vector2(pos.x, max.y);
        Vector2 screenTop = new Vector2(Screen.width / 2f, Screen.height);

        line.position = screenTop;

        Vector2 dir = (screenTop - topCenter);
        float length = dir.magnitude;

        line.sizeDelta = new Vector2(line.sizeDelta.x, length);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        line.rotation = Quaternion.Euler(0, 0, angle);
    }
}

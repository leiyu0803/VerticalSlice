using UnityEngine;
using UnityEngine.InputSystem;

public class AimUI : MonoBehaviour
{
    public Camera cam;
    public RectTransform canvasRect;
    public GameObject uiPoint;

    public Transform worldTarget;

    AimStateManager aimStateManager;
    private void Start()
    {
        aimStateManager = GetComponentInParent<AimStateManager>();
    }
    void Update()
    {
        if (aimStateManager.currentState == aimStateManager.aim)
        {
            uiPoint.SetActive(true);
        }
        else
        {
            uiPoint.SetActive(false);
            return;
        }
        Vector3 screenPos = cam.WorldToScreenPoint(worldTarget.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            null,
            out Vector2 localPos
        );

        uiPoint.GetComponent<RectTransform>().anchoredPosition = localPos;

    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class AimUI : MonoBehaviour
{
    public Camera cam;
    public RectTransform canvasRect;
    public GameObject uiPoint;

    public GameObject actualAimUIPoint;

    public Transform worldTarget;
    public Transform worldTarget2;

    AimStateManager aimStateManager;

    [HideInInspector] public bool IsDifferent;
    private void Start()
    {
        aimStateManager = GetComponentInParent<AimStateManager>();
    }
    void Update()
    {
        if (aimStateManager.currentState == aimStateManager.aim)
        {
            uiPoint.SetActive(true);
            actualAimUIPoint.SetActive(IsDifferent);
        }
        else
        {
            uiPoint.SetActive(false);
            actualAimUIPoint.SetActive(false);
            return;
        }
        Vector3 screenPos = cam.WorldToScreenPoint(worldTarget.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            null,
            out Vector2 localPos
        );
        Vector3 screenPos1 = cam.WorldToScreenPoint(worldTarget2.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos1,
            null,
            out Vector2 localPos1
        );

        uiPoint.GetComponent<RectTransform>().anchoredPosition = localPos;
        actualAimUIPoint.GetComponent<RectTransform>().anchoredPosition = localPos1;
    }
}
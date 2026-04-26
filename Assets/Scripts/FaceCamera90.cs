using UnityEngine;

public class FaceCamera90 : MonoBehaviour
{
    Transform targetCamera;

    float snapIncrement = 90f;

    private void Update()
    {
        targetCamera = Camera.main.transform;

        Vector3 dir = targetCamera.position - transform.position;

        float angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg - 90f;
        angle = Mathf.Round(angle / snapIncrement) * snapIncrement;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 10f);
    }
}
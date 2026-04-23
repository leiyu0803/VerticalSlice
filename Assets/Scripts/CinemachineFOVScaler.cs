using UnityEngine;
using Unity.Cinemachine;

public class CinemachineFOVScaler : MonoBehaviour
{
    public CinemachineCamera vcam;
    public float referenceFOV = 60f; 

    private float baseScale;

    void Start()
    {
        baseScale = transform.localScale.x; 
    }

    void Update()
    {
        float currentFOV = vcam.Lens.FieldOfView;

        float scaleFactor =
            Mathf.Tan(currentFOV * 0.5f * Mathf.Deg2Rad) /
            Mathf.Tan(referenceFOV * 0.5f * Mathf.Deg2Rad);

        transform.localScale = Vector3.one * baseScale * scaleFactor;
    }
}

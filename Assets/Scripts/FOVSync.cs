using UnityEngine;

public class FOVSync : MonoBehaviour
{
    Camera mainCamera;
    Camera thisCamera;
    void Start()
    {
        mainCamera = Camera.main;
        thisCamera = GetComponent<Camera>();
    }
    void Update()
    {
        thisCamera.fieldOfView = mainCamera.fieldOfView;
    }
}

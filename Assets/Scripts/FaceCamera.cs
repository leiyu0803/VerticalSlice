using UnityEngine;
using UnityEngine.InputSystem;

public class FaceCamera : MonoBehaviour
{
    public GameObject cam;
    PlayerInput playerInput;
    MovementStateManager controller;
    float xPos,yRot,z;

    private void Start()
    {
        xPos = transform.localPosition.x;
        yRot = 25;
        z = 1;
        playerInput = GetComponentInParent<PlayerInput>();
        controller = GetComponentInParent<MovementStateManager>();
    }

    void Update()
    {
        float pitch = cam.transform.eulerAngles.x;
        if(pitch>180)
            pitch -= 360;
        float roll = pitch / 3;
        if (playerInput.actions["Switch"].WasPressedThisFrame())
        {
            xPos = -xPos;
            yRot = -yRot;
            z = -z;
        }
        Vector3 newFollowPos = new Vector3(xPos, cam.transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newFollowPos, 10 * Time.deltaTime);
        Quaternion newRot = Quaternion.Euler(pitch, yRot, roll * z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, 10 * Time.deltaTime);

    }

}

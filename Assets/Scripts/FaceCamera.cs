using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public GameObject cam;

    void Update()
    {
        float pitch = cam.transform.eulerAngles.x;
        if(pitch>180)
            pitch -= 360;
        float roll = pitch / 3;
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
        transform.localPosition = new Vector3(transform.localPosition.x, cam.transform.localPosition.y, transform.localPosition.z);
    }
}

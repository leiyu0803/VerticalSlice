using UnityEngine;

public class AngleFix : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y <= 45)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 45, transform.rotation.eulerAngles.z);

        }
    }
}

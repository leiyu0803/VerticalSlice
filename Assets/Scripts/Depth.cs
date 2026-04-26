using UnityEngine;

public class Depth : MonoBehaviour
{
    [SerializeField] float depth;
    void Start()
    {
        GetComponent<Camera>().depth = depth;
    }
}

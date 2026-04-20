using System;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    float currentLifetime;

    // Update is called once per frame
    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}

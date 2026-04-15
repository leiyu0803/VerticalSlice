using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 5;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

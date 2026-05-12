using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialProgress tutorialProgress;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tutorialProgress.progress++;
            Destroy(gameObject);
        }
    }
}

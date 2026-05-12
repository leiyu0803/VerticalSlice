using UnityEngine;

public class DevItemEnemySpawner : InteractableItem
{
    public GameObject enemyPrefab;
    public Transform enemySpawnLocation;
    bool isUsed = false;
    public override void Interact()
    {
        Instantiate(enemyPrefab, enemySpawnLocation.position, enemySpawnLocation.rotation);
        if (!isUsed)
        {
            GameObject a = GameObject.Find("TutorialProgress");
            if (a != null && !isUsed)
            {
                a.GetComponent<TutorialProgress>().progress++;
                isUsed = true;
            }
        }
    }
}

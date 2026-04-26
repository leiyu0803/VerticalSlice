using UnityEngine;

public class InteractableItemHeal : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<ItemManager>().heal += 1;
    }
}

using UnityEngine;

public class InteractableItemBattery : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<ItemManager>().battery += 1;
    }
}

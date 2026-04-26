using UnityEngine;

public class InteractableItemWater : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<ItemManager>().water += 1;
    }
}

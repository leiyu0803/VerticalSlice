using UnityEngine;

public class InteractableItemOBJ : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<ItemManager>().hasOBJ = true;
    }
}

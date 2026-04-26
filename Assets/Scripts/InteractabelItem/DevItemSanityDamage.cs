using UnityEngine;

public class DevItemSanityDamage : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<PlayerHealthManager>().SanityDamage(20);
    }
}

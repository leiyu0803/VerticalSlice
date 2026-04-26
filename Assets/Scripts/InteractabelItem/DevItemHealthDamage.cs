using UnityEngine;
using UnityEngine.UI;

public class DevItemHealthDamage : InteractableItem
{
    public override void Interact()
    {
        player.GetComponent<PlayerHealthManager>().TakeDamage(20);
    }
}

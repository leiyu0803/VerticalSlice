using UnityEngine;
using System.Collections;
public class TutorialProgress : Progress
{
    void Start()
    {
        GameObject line = Instantiate(captionLinePrefab, captionLineParent);
        line.GetComponent<CaptionLine>().SetText("Welcome, new recruit. Before you officially join the operation, I’ll walk you through our procedures.");
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (progress == 0 && timer > 5)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Let's get started with the basics. Now head to the shooting range.");
            objText.SetText("• Go to the shooting range");
            hintText.SetText("Hold WSAD to move around\nMove Mouse to look around\nHold Left Shift to run");
            doors[0].SetActive(false);
            progress = 1;
        }
        else if (progress == 2)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Great! Now pick up the ammo.");
            objText.SetText("• Pick up the ammo");
            hintText.SetText("Hold F to pick up items");
            progress = 3;
        }
        else if (progress == 3 && player.GetComponent<ActionStateManager>().currentWeapon.GetComponent<WeaponAmmo>().extraAmmo > 0)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Well done! Now reload your weapon.");
            objText.SetText("• Reload your weapon");
            hintText.SetText("Press R to reload");
            progress = 4;
        }
        else if (progress == 4 && player.GetComponent<ActionStateManager>().currentWeapon.GetComponent<WeaponAmmo>().currentAmmo > 0)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            foreach (GameObject target in Targets)
            {
                target.SetActive(true);
            }
            line.GetComponent<CaptionLine>().SetText("Excellent! Now shoot the target.");
            objText.SetText("• Shoot the target");
            hintText.SetText("Hold Left Mouse Button to shoot\nHold Right Mouse Button to aim");
            progress = 5;
        }
        else if (progress == 7)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Now, let's move on to the next area, where I'll show you how to treat injuries.");
            objText.SetText("• Go to the medical area");
            hintText.SetText("Press T to switch shoulder");
            doors[1].SetActive(false);
            progress = 8;
        }
        else if (progress == 9)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("I'm going to let you get hurt a little so you can learn how to treat your injuries. Now pick up the first-aid kit and water on the table.");
            objText.SetText("• Pick up the first-aid kit and water");
            hintText.SetText("");
            player.GetComponent<PlayerHealthManager>().TakeDamage(20, gameObject);
            player.GetComponent<PlayerHealthManager>().SanityDamage(20, gameObject);
            progress = 10;
        }
        else if (progress == 10 && player.GetComponent<ItemManager>().heal > 0 && player.GetComponent<ItemManager>().water > 0)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Good job! Now use the first-aid kit to heal yourself and water to make you calm.");
            objText.SetText("• Heal yourself");
            hintText.SetText("Hold 3 to use first-aid kit\nHold 4 to use water\nYou can only do this when\nyour Health or Sanity is not full");
            progress = 11;
        }
        else if (progress == 11 && player.GetComponent<PlayerHealthManager>().GetHealth() == player.GetComponent<PlayerHealthManager>().maxHealth && player.GetComponent<PlayerHealthManager>().GetSanity() == player.GetComponent<PlayerHealthManager>().maxSanity)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Excellent! Proceed to the next area, and I’ll brief you on the threats in the operational zone and your mission.");
            objText.SetText("• Proceed to the next area");
            hintText.SetText("");
            doors[2].SetActive(false);
            progress = 12;
        }
        else if (progress == 13)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Press the button on the wall to generate a simulated enemy.");
            objText.SetText("• Spawn an enemy");
            hintText.SetText("");
            progress = 14;
        }
        else if (progress == 15)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("We still don’t know how these enemies came to be, but fortunately, regular weapons are effective against them.");
            objText.SetText("• Kill the enemy");
            hintText.SetText("The enemy will start chasing you\n as soon as they see you\nDealing enough damage will kill the enemy\nPress 5 to scan the enemy");
            doors[3].SetActive(false);
            progress = 16;
        }
        else if (progress == 17)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Your mission is to investigate the operational zone. In this training, your objective is to secure a unknown container.");
            objText.SetText("• Pick up the unknown container");
            hintText.SetText("");
            progress = 18;
        }
        else if (progress == 18 && player.GetComponent<ItemManager>().hasOBJ)
        {
            GameObject line = Instantiate(captionLinePrefab, captionLineParent);
            line.GetComponent<CaptionLine>().SetText("Once you've completed the main objective, you can head to the evacuation point to evacuate.");
            objText.SetText("• Evacuate");
            hintText.SetText("The evacuation point looks like a phone booth.");
            doors[4].SetActive(false);
            progress = 19;
        }
        else if(progress == 20)
        {
            objText.SetText("");
            hintText.SetText("");
            EP();
        }
    }
}
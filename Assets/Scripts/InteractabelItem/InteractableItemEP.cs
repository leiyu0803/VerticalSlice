using UnityEngine;

public class InteractableEP : InteractableItem
{
    Progress progressScript;
    public int progressNum;
    public override void Start()
    {
        base.Start();
        progressScript = GameObject.FindWithTag("GameController").GetComponent<Progress>();
        isActive = false;
    }
    public override void Interact()
    {
        progressScript.progress++;
    }
    public override void Update()
    {
        base.Update();
        if(progressNum == progressScript.progress)
        {
            isActive = true;
        }
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableItemManager : MonoBehaviour
{
    public List<GameObject> interactables  = new List<GameObject>();
    GameObject MinDistanceObject;
    float MinDistance;
    GameObject LasFrameMinDistanceObject;
    PlayerInput playerInput;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        MinDistanceObject = null;
        MinDistance = Mathf.Infinity;
        foreach (GameObject interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);
            if (distance < MinDistance)
            {
                MinDistance = distance;
                MinDistanceObject = interactable;

            }
        }
        if(MinDistanceObject != LasFrameMinDistanceObject)
        {
            if (LasFrameMinDistanceObject != null)
            {
                LasFrameMinDistanceObject.GetComponent<InteractableItem>().OnInteractiveDeactive();
            }
            if (MinDistanceObject != null)
            {
                MinDistanceObject.GetComponent<InteractableItem>().OnInteractiveActive();
            }
        }
        LasFrameMinDistanceObject = MinDistanceObject;
        if(playerInput.actions["Interact"].WasPressedThisFrame())
        {
            if(MinDistanceObject != null)
            {
                MinDistanceObject.GetComponent<InteractableItem>().InteractStart();
            }
        }
        if(playerInput.actions["Interact"].WasReleasedThisFrame())
        {
            if(MinDistanceObject != null)
            {
                MinDistanceObject.GetComponent<InteractableItem>().InteractEnd();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] private float interactTime = 1f;
    private float CurrentInteractTime;
    private bool interacting = false;
    Image[] interactImages;
    Image interactImage;
    [HideInInspector] public GameObject player;
    [SerializeField] bool DistoryAfterInteract = true;
    public bool isActive = true;
    public virtual void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        interactImages = canvas.GetComponentsInChildren<Image>();
        foreach (Image image in interactImages) 
        {
            if(image.tag == "InteractImage")
            {
                interactImage = image;
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&isActive)
        {
            other.GetComponent<InteractableItemManager>().interactables.Add(gameObject);
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<InteractableItemManager>().interactables.Remove(gameObject);
        }
    }
    public void OnInteractiveActive()
    {
        canvas.enabled = true;
    }
    public void OnInteractiveDeactive()
    {
        canvas.enabled = false;
    }
    public void InteractStart()
    {
        interacting = true;
    }
    public void InteractEnd()
    {
        interacting = false;
    }
    public virtual void Update()
    {
        if (interacting)
        {
            CurrentInteractTime += Time.deltaTime;
            interactImage.fillAmount = CurrentInteractTime / interactTime;
            if (CurrentInteractTime >= interactTime)
            {
                Interact();
                if (DistoryAfterInteract)
                {
                    GameObject.Destroy(gameObject);
                    player.GetComponent<InteractableItemManager>().interactables.Remove(gameObject);
                }
                else
                {
                    InteractEnd();
                    CurrentInteractTime = 0;
                }
            }
        }
        else
        {
            CurrentInteractTime = 0;
            interactImage.fillAmount = Mathf.Lerp(interactImage.fillAmount, 0, Time.deltaTime * 10);
        }
    }
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}

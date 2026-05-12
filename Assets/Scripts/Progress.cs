using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Progress : MonoBehaviour
{
    public GameObject player;
    public float progress;
    public GameObject captionLinePrefab;
    public Transform captionLineParent;
    public TMP_Text objText;
    public TMP_Text hintText;

    public List<GameObject> doors;
    public List<GameObject> Targets;

    public float timer = 0;
    public GameObject endPanel;

    public void EP()
    {
        player.SetActive(false);
        endPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

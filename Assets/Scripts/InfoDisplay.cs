using System;
using TMPro;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    TMP_Text text;
    string Verision;
    DateTime time;
    RuntimePlatform platform;
    string ID;
    string productName;
    string sceneName;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        Verision = Application.version;
        platform = Application.platform;
        ID = Application.buildGUID;
        productName = Application.productName;
    }
    void Update()
    {
        time = DateTime.UtcNow;
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        text.text = productName + "\n" + sceneName + "\n" + time + " UTC" + "\n" + "ENG_" + platform + "_" + Verision + "_" + ID;
    }
}

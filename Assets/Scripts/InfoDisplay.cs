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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        Verision = Application.version;
        platform = Application.platform;
        ID = Application.buildGUID;
        productName = Application.productName;
    }

    // Update is called once per frame
    void Update()
    {
        time = DateTime.UtcNow;
        text.text = productName + "\n" + "ENG_" + platform + "_" + Verision + "_" + ID + "\n" + time + " UTC";
    }
}

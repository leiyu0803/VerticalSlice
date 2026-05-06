using TMPro;
using UnityEngine;

public class DamageSourceUI : MonoBehaviour
{
    public TMP_Text sourceName;
    public TMP_Text value;

    public void SetDamageSource(string Name, float Value)
    {
        sourceName.text = Name;
        value.text = Value.ToString();
    }
}

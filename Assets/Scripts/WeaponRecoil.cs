using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilFollowPos;
    [SerializeField] float kickbackAmount = -1;
    [SerializeField] float kickbackSpeed = 10;
    [SerializeField] float returnSpeed = 20;
    float currentRecoilPos;
    float finalRecoilPos;

    void Update()
    {
        currentRecoilPos = Mathf.Lerp(currentRecoilPos, 0, returnSpeed * Time.deltaTime);
        finalRecoilPos = Mathf.Lerp(finalRecoilPos, currentRecoilPos, kickbackSpeed * Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPos);
    }

    public void Recoil() 
    {
        currentRecoilPos += kickbackAmount;
    } 
}

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public GameObject warning;
    public GameObject StartButton;
    bool IsWarning = false;
    public TMP_Text progress;
    bool Started = false;
    public void GameStart()
    {
        IsWarning = true;
        StartButton.SetActive(false);
    }
    private void Update()
    {
        if(IsWarning)
            warning.transform.localScale = new Vector3(1, Mathf.Lerp(warning.transform.localScale.y, 1, Time.deltaTime * 20), 1);
        if(!Started&& warning.transform.localScale.y>=0.999f)
        {
            Started = true;
            StartCoroutine(LoadLevel());
        }
    }
    IEnumerator LoadLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            progress.text = "Loading: " + asyncLoad.progress * 100 + "%";
            if(asyncLoad.progress >= 0.9f)
            {
                progress.gameObject.GetComponent<Flashing>().IsFlashing = true;
                progress.text = "Press Any Key to Continue";
                if(Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    public void Continue()
    {
    }
}

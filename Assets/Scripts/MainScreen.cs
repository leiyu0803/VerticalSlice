using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public GameObject warning;
    public List<GameObject> StartButton;
    bool IsWarning = false;
    public TMP_Text progress;
    bool Started = false;
    int level;
    public void GameStart(int level)
    {
        IsWarning = true;
        foreach (GameObject StartButton in StartButton)
            StartButton.SetActive(false);
        this.level = level;
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
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
}

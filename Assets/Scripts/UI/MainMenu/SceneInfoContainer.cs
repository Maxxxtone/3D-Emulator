using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInfoContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sceneText, _sceneNumber;
    [SerializeField] private Image _progressBarFill;
    public string sceneName;
    public void CreateSceneInfo(string sceneName)
    {
        this.sceneName = sceneName;
        _sceneText.text = sceneName;
    }
    public void SelectScene()
    {
        ScenesPanel.instance.Save();
        PlayerPrefs.SetString("SceneName", sceneName);
        StartCoroutine(LoadingScene());
        //SceneManager.LoadScene(1);
    }
    public void DeleteSceneInfo()
    {
        ScenesPanel.instance.scenesInfo.Remove(this);
        Destroy(gameObject);
    }
    private IEnumerator LoadingScene()
    {
        var operation = SceneManager.LoadSceneAsync(1);
        _progressBarFill.fillAmount = 0;
        while (!operation.isDone)
        {
            _progressBarFill.fillAmount = operation.progress;
            yield return null;
        }
    }
}

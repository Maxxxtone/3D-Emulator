using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneInfoContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sceneText, _sceneNumber;
    public string sceneName;
    public void CreateSceneInfo(string sceneName)
    {
        this.sceneName = sceneName;
        _sceneText.text = sceneName;
    }
    public void SelectScene()
    {
        print(sceneName);
        PlayerPrefs.SetString("SceneName", sceneName);
        SceneManager.LoadScene(1);
    }
    public void DeleteSceneInfo()
    {
        ScenesPanel.instance.scenesInfo.Remove(this);
        Destroy(gameObject);
    }
}

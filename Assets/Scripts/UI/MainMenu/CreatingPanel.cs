using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatingPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _sceneNameInput;
    public void CreateSceneInfo()
    {
        if(_sceneNameInput.text == string.Empty)
        {
            _sceneNameInput.caretColor = Color.red;
            return;
        }
        if (!ScenesPanel.instance.NameIsUnique(_sceneNameInput.text))
            return;
        ScenesPanel.instance.CreateContainer(_sceneNameInput.text);
        _sceneNameInput.caretColor = Color.red;
        gameObject.SetActive(true);
    }
}

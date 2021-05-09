using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScenesPanel : MonoBehaviour
{
    public static ScenesPanel instance;
    public List<SceneInfoContainer> scenesInfo;
    [SerializeField] public SceneInfoContainer _sceneInfoPrefab;
    private string _filePath;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _filePath = Application.persistentDataPath + "/scenes.data";
        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);
        ScenesData save = new ScenesData();
        save.SaveScene(scenesInfo);
        bf.Serialize(fs, save);
        fs.Close();
    }
    private void Load()
    {
        if (!File.Exists(_filePath))
            return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);
        ScenesData save = (ScenesData)bf.Deserialize(fs);
        fs.Close();
        foreach (var s in save.sceneNames)
            CreateContainer(s);
    }
    public void CreateContainer(string name)
    {
            var container = Instantiate(_sceneInfoPrefab, transform.position, Quaternion.identity);
            container.transform.parent = transform;
            container.transform.localScale = Vector3.one;
            container.CreateSceneInfo(name);
            scenesInfo.Add(container);
        print("container created");
    }
    public bool NameIsUnique(string name)
    {
        foreach(var n in scenesInfo)
        {
            if (n.sceneName == name)
            {
                return false;
            }
        }
        return true;
    }
}
[System.Serializable]
public class ScenesData
{
    public List<string> sceneNames = new List<string>();
    public void SaveScene(List<SceneInfoContainer> scenes)
    {
        foreach(var s in scenes)
        {
            sceneNames.Add(s.sceneName);
        }
    }
}
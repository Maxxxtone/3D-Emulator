using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public List<RaycastTarget> things;
    private string _filePath;
    [SerializeField] private GameObject _manipulatorPrefab, _trafficLightsPrefab, _remoteTerminalPrefab, _detailPrefab;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _filePath = Application.persistentDataPath + "/thigs"+PlayerPrefs.GetString("SceneName") + ".data";
        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);
        Save save = new Save();
        save.SaveThings(things);
        bf.Serialize(fs, save);
        fs.Close();
    }
    private void Load()
    {
        if (!File.Exists(_filePath))
            return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();
        foreach(var data in save.thingsData)
        {
            things.Add(SpawnObjectByType(data));
        }
        print("load");
    }
    private RaycastTarget SpawnObjectByType(Save.ThingSaveData data)
    {
        RaycastTarget thing = null;
        switch (data.Type)
        {
            case ThingType.Manipulator:
                thing = InstantiatePrefab(_manipulatorPrefab, data);
                break;
            case ThingType.Lamp:
                thing = InstantiatePrefab(_trafficLightsPrefab, data);
                break;
            case ThingType.RemoteTerminal:
                thing = InstantiatePrefab(_remoteTerminalPrefab, data);
                break;
            case ThingType.Figure:
                thing = InstantiatePrefab(_detailPrefab, data);
                break;
        }
        return thing;
    }
    private RaycastTarget InstantiatePrefab(GameObject prefab, Save.ThingSaveData data)
    {
        var thing = Instantiate(prefab, transform.position, Quaternion.identity);
        thing.transform.position = new Vector3(data.Position.x, data.Position.y, data.Position.z);
        thing.transform.eulerAngles = new Vector3(data.Rotaion.x, data.Rotaion.y, data.Rotaion.z);
        return thing.GetComponent<RaycastTarget>();
    }
}
[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    [System.Serializable]
    public struct ThingSaveData
    {
        public ThingType Type;
        public Vec3 Position, Rotaion;

        public ThingSaveData(Vec3 position, Vec3 rotaion, ThingType type)
        {
            Type = type;
            Position = position;
            Rotaion = rotaion;
        }
    }
    public List<ThingSaveData> thingsData = new List<ThingSaveData>();
    public void SaveThings(List<RaycastTarget> things)
    {
        foreach (var t in things)
        {
            var type = t.Type;
            Vec3 pos = new Vec3(t.transform.position.x, t.transform.position.y, t.transform.position.z);
            Vec3 rot = new Vec3(t.transform.eulerAngles.x, t.transform.eulerAngles.y, t.transform.eulerAngles.z);
            thingsData.Add(new ThingSaveData(pos, rot, type));
        }
    }
}

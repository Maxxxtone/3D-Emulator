using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnThingButton : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn, _objectToMove;
    private Button _spawnButton;
    private void Start()
    {
        _spawnButton = GetComponent<Button>();
        _spawnButton.onClick.AddListener(delegate { Placer.instance.SetObjectToSpawn(_objectToMove, _objectToSpawn); });
    }
}

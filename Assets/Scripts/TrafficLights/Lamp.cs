using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool isOn;
    [SerializeField] private Material _defaultMaterial, _lightMaterial;
    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetLampState(false);
    }
    public void SetLampState(bool lightOn)
    {
        isOn = lightOn;
        _renderer.material = _defaultMaterial;
        if (lightOn)
            _renderer.material = _lightMaterial;
    }
}

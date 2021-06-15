using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightConnection : ThingConnection
{
    private TrafficLightController _controller;
    protected override void Start()
    {
        _controller = GetComponent<TrafficLightController>();
        base.Start();
    }
    protected override void SetDataForController()
    {
        if (connectionState && _setData)
            SetLampsState();
    }
    private void SetLampsState()
    {
        for (int i = 0; i < _controller._lamps.Length; i++)
        {
            bool isOn = false;
            if (_data["L" + (i + 1)] == 1)
                isOn = true;
            _controller._lamps[i].SetLampState(isOn);
        }
    }
}

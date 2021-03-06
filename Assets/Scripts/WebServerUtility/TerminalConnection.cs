using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using SimpleJSON;

public class TerminalConnection : ThingConnection
{
    private RemoteTerminal _controller;

    protected override void Start()
    {
        _controller = GetComponent<RemoteTerminal>();
        base.Start();
    }
    protected override JSONObject CreateInputsForController()
    {
        JSONObject inputs = new JSONObject();
        for (int i = 1; i < 4; i++)
        {
            var key = "b" + i;
            inputs.AddField(key, _controller.GetButtonClicks(key));
        }
        inputs.AddField("p", _controller._dmsState);
        return inputs;
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

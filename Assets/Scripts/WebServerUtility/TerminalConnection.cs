using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using SimpleJSON;

public class TerminalConnection : MonoBehaviour
{
    public bool connectionState;
    private RemoteTerminal controller;
    private float _inputsTimer = 3f;
    private JSONNode data;
    private bool _setData;
    WebSocket ws;
    public void SetName(string thingName)
    {
        gameObject.name = thingName;
    }
    public void Connect(bool state)
    {
        connectionState = state;
        if (state)
            ConnectToSocket();
        else
            DisconnectFromSocket();
    }

    private void Start()
    {
        controller = GetComponent<RemoteTerminal>();
        _inputsTimer = 1f;
        ws = new WebSocket("ws://localhost:4000");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("rec from " + ((WebSocket)sender).Url + " data: " + e.Data);
            JSONNode objectInfo = JSON.Parse(e.Data);
            string type = objectInfo["type"];
            print(objectInfo);
            if (type == "outputs")
            {
                _setData = true;
                data = objectInfo["data"]["outputs"];
            }
            else
                _setData = false;
        };
        ws.Connect();
    }
    private void Update()
    {
        if (ws == null)
            return;
        else
        {
            if (_inputsTimer <= 0 && connectionState)
            {
                SetDataForController();
                JSONObject obj = new JSONObject();
                obj.AddField("type", "inputs");
                JSONObject inputs = new JSONObject();
                inputs.AddField("b1", Random.Range(0, 100));
                inputs.AddField("b2", Random.Range(0, 100));
                inputs.AddField("b3", Random.Range(0, 100));
                inputs.AddField("p", Random.Range(0, 2));
                JSONObject data = new JSONObject();
                data.AddField("name", gameObject.name);
                data.AddField("inputs", inputs);
                obj.AddField("data", data);
                ws.Send(obj.ToString());
                _inputsTimer = 2f;
            }
            else
                _inputsTimer -= Time.deltaTime;
        }
    }
    private void SetDataForController()
    {
        if (connectionState && _setData)
        {
            bool a = false;
            if (data["L1"] == 1)
                a = true;
            controller._lamps[0].SetLampState(a);
        }
    }
    private void ConnectToSocket()
    {
        JSONObject obj = new JSONObject();
        obj.AddField("type", "connect");
        JSONObject aa = new JSONObject();
        aa.AddField("name", gameObject.name);
        aa.AddField("app_key", "40dc8315-aae6-4b9b-a53a-d3b31ae27508");
        aa.AddField("url", "https://pp-2105240757me.devportal.ptc.io");
        obj.AddField("data", aa);
        ws.Send(obj.ToString());
    }
    private void DisconnectFromSocket()
    {
        JSONObject obj = new JSONObject();
        obj.AddField("type", "remove");
        JSONObject aa = new JSONObject();
        aa.AddField("name", gameObject.name);
        obj.AddField("data", aa);
        ws.Send(obj.ToString());
    }
}

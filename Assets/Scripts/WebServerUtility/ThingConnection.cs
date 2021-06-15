using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using WebSocketSharp;

public class ThingConnection : MonoBehaviour
{
    public bool connectionState;
    protected float _inputsTimer = 3f;
    protected JSONNode _data;
    protected bool _setData;
    protected WebSocket _webSocket;

    protected virtual void Start()
    {
        _data = null;
        ConnectThingToSocket();
    }
    protected virtual void Update()
    {
        if (_webSocket == null)
            return;
        else
        {
            if (_inputsTimer <= 0 && connectionState)
            {
                SetDataForController();
                var inputs = CreateInputsForController();
                SendDataToServer(inputs);
            }
            else
                _inputsTimer -= Time.deltaTime;
        }
    }

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
    protected void ConnectThingToSocket()
    {
        _webSocket = new WebSocket("ws://localhost:4000");
        _inputsTimer = 0f;
        _webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("rec from " + ((WebSocket)sender).Url + " data: " + e.Data);
            JSONNode objectInfo = JSON.Parse(e.Data);
            string type = objectInfo["type"];
            if (type == "outputs")
            {
                _setData = true;
                _data = objectInfo["data"]["outputs"];
            }
            else
                _setData = false;
        };
        _webSocket.Connect();
    }
    protected void SendDataToServer(JSONObject inputs)
    {
        JSONObject obj = new JSONObject();
        obj.AddField("type", "inputs");
        JSONObject data = new JSONObject();
        data.AddField("name", gameObject.name);
        data.AddField("inputs", inputs);
        obj.AddField("data", data);
        _webSocket.Send(obj.ToString());
        _inputsTimer = 2f;
    }
    protected void ConnectToSocket()
    {
        JSONObject obj = new JSONObject();
        obj.AddField("type", "connect");
        JSONObject request = new JSONObject();
        request.AddField("name", gameObject.name);
        request.AddField("app_key", SceneConnectionInfo.instance.ApplicationKey);
        request.AddField("url", SceneConnectionInfo.instance.ServerUrl);
        obj.AddField("data", request);
        _webSocket.Send(obj.ToString());
    }
    protected void DisconnectFromSocket()
    {
        JSONObject obj = new JSONObject();
        obj.AddField("type", "remove");
        JSONObject request = new JSONObject();
        request.AddField("name", gameObject.name);
        obj.AddField("data", request);
        _webSocket.Send(obj.ToString());
    }
    protected virtual JSONObject CreateInputsForController() => new JSONObject();
    protected virtual void SetDataForController() { }
}

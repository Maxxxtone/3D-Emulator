using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using WebSocketSharp;
using TMPro;

public class RobotConnection : MonoBehaviour
{
    public bool connectionState;
    private RobotController controller;
    private float _inputsTimer = 3f;
    private JSONNode data;
    private bool _setData;
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

    WebSocket ws;
    private void Start()
    {
        controller = GetComponent<RobotController>();
        _inputsTimer = 1f;
        ws = new WebSocket("ws://localhost:4000");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("rec from " + ((WebSocket)sender).Url + " data: " + e.Data);
            JSONNode objectInfo = JSON.Parse(e.Data);
            string type = objectInfo["type"];
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
        {
            print("ws nll");
            return;
        }
        else
        {
            if (_inputsTimer <= 0 && connectionState)
            {
                SetDataForController();
                JSONObject obj = new JSONObject();
                obj.AddField("type", "inputs");
                JSONObject inputs = new JSONObject();
                inputs.AddField("m1", Random.Range(0, 100));
                inputs.AddField("m2", Random.Range(0, 100));
                inputs.AddField("m3", Random.Range(0, 100));
                inputs.AddField("m4", Random.Range(0, 100));
                inputs.AddField("m5", Random.Range(0, 100));
                inputs.AddField("m6", Random.Range(0, 100));
                inputs.AddField("l1", 100);
                inputs.AddField("l2", 200);
                inputs.AddField("l3", 100);
                inputs.AddField("l4", 100);
                inputs.AddField("l5", 100);
                inputs.AddField("l6", 100);
                inputs.AddField("t1", 100);
                inputs.AddField("t2", 100);
                inputs.AddField("t3", 100);
                inputs.AddField("t4", 100);
                inputs.AddField("t5", 100);
                inputs.AddField("t6", 100);
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
            controller.ChangeDistance(data["Y"]);
            controller.ChangeBaseRotation(data["X"], true);
            controller.ChangeGripperRotation(data["T"]);
            controller.SetGripperElevation((int)data["G"]);
            controller.Grip((int)data["V"]);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using WebSocketSharp;
using TMPro;

public class RobotConnection : ThingConnection
{
    private RobotController _controller;
    protected override void Start()
    {
        _controller = GetComponent<RobotController>();
        base.Start();
    }
    protected override void SetDataForController()
    {
        if (_data.IsNull) return;
        _controller.ChangeDistance((int)_data["Y"]);
        _controller.ChangeBaseRotation(_data["X"], true);
        _controller.ChangeGripperRotation(_data["T"]);
        _controller.SetGripperElevation((int)_data["G"]);
        _controller.Grip((int)_data["V"]);
    }
    protected override JSONObject CreateInputsForController()
    {
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
        return inputs;
    }
}

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
        var motors = new int[] {
            Random.Range(0, 4048),
            Random.Range(0, 4048),
            Random.Range(0, 4048),
            Random.Range(0, 4048),
            Random.Range(0, 4048),
            Random.Range(0, 4048),
        };
        var load = new int[]{
            Random.Range(0,1024),
            Random.Range(0,1024),
            Random.Range(0,1024),
            Random.Range(0,1024),
            Random.Range(0,1024),
            Random.Range(0,1024),
        };
        var temp = new int[]{
            Random.Range(0,100),
            Random.Range(0,100),
            Random.Range(0,100),
            Random.Range(0,100),
            Random.Range(0,100),
            Random.Range(0,100)
        };
        ManipulatorPanel.instance.UpdateMotorLabels(motors);
        ManipulatorPanel.instance.UpdateLoadLabels(load);
        ManipulatorPanel.instance.UpdateTemoperatureLabels(temp);
        JSONObject inputs = new JSONObject();
        inputs.AddField("m1", motors[0]);
        inputs.AddField("m2", motors[1]);
        inputs.AddField("m3", motors[2]);
        inputs.AddField("m4", motors[3]);
        inputs.AddField("m5", motors[4]);
        inputs.AddField("m6", motors[5]);
        inputs.AddField("l1", load[0]);
        inputs.AddField("l2", load[1]);
        inputs.AddField("l3", load[2]);
        inputs.AddField("l4", load[3]);
        inputs.AddField("l5", load[4]);
        inputs.AddField("l6", load[5]);
        inputs.AddField("t1", temp[0]);
        inputs.AddField("t2", temp[1]);
        inputs.AddField("t3", temp[2]);
        inputs.AddField("t4", temp[3]);
        inputs.AddField("t5", temp[4]);
        inputs.AddField("t6", temp[5]);
        return inputs;
    }
}

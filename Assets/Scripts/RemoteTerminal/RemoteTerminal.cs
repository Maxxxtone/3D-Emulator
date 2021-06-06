using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTerminal : TrafficLightController
{
    public int _dmsState, _b1, _b2, _b3;
    public TerminalConnection connection;
    private void Start()
    {
        connection = GetComponent<TerminalConnection>();
    }
}

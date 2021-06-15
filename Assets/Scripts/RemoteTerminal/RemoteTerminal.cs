using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTerminal : TrafficLightController
{
    public int _dmsState;
    private Dictionary<string, int> _buttons = new Dictionary<string, int>();
    public TerminalConnection connection;
    private void Start()
    {
        connection = GetComponent<TerminalConnection>();
        for (int i = 1; i < 4; i++)
            _buttons.Add("b" + i, 0);
    }
    public void SetButtonClicks(string buttonName, int clicks) => _buttons[buttonName] = clicks;
    public int GetButtonClicks(string buttonName) => _buttons[buttonName];
}

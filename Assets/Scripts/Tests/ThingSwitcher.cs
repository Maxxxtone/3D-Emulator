using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThingType
{
    Manipulator,
    Figure,
    Lamp,
    RemoteTerminal
}
public class ThingSwitcher : MonoBehaviour
{
    public static ThingSwitcher instance;
    private GameObject _currentThing;
    private void Awake()
    {
        instance = this;
    }
    public void SwitchThing(ThingType type, GameObject thing)
    {
        _currentThing = thing;
        switch (type)
        {
            case ThingType.Manipulator:
                ControlPanelSwitcher.instance.ShowManipulatorPanel();
                ManipulatorPanel.instance.ChangeManipulator(thing.GetComponent<RobotController>());
                break;
            case ThingType.Lamp:
                ControlPanelSwitcher.instance.ShowTrafficLightPanel();
                TrafficLightPanel.instance.ChangeTrafficLight(thing.GetComponent<TrafficLightController>());
                break;
            case ThingType.RemoteTerminal:
                ControlPanelSwitcher.instance.ShowRemoteTerminalPanel();
                RemoteTerminalPanel.instance.ChangeTrafficLight(thing.GetComponent<RemoteTerminal>());
                break;
        }
    }
    public void DeleteActiveThing()
    {
        if (_currentThing != null)
        {
            SaveSystem.instance.things.Remove(_currentThing.GetComponent<RaycastTarget>());
            Destroy(_currentThing);
        }
    }
}

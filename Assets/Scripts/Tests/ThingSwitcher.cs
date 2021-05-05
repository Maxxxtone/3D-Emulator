using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThingType
{
    Manipulator
}
public class ThingSwitcher : MonoBehaviour
{
    public static ThingSwitcher instance;
    private void Awake()
    {
        instance = this;
    }
    public void SwitchThing(ThingType type, GameObject thing)
    {
        switch (type)
        {
            case ThingType.Manipulator:
                ManipulatorPanel.instance.ChangeManipulator(thing.GetComponent<RobotController>());
                break;
        }
        
    }
}

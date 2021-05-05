using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulatorPanel : MonoBehaviour
{
    public static ManipulatorPanel instance;
    [SerializeField] private Slider _rotationBaseSlider, _armDistanceSlider, _gripperRotationSlider;
    [SerializeField] private Toggle _elevationToggle, _gripToggle;
    private void Awake()
    {
        instance = this;
    }
    public void ChangeManipulator(RobotController controller)
    {
        _rotationBaseSlider.onValueChanged.RemoveAllListeners();
        _armDistanceSlider.onValueChanged.RemoveAllListeners();
        _gripperRotationSlider.onValueChanged.RemoveAllListeners();
        _elevationToggle.onValueChanged.RemoveAllListeners();
        _gripToggle.onValueChanged.RemoveAllListeners();
        _rotationBaseSlider.value = controller.rotationX;
        _armDistanceSlider.value = controller.distanceY;
        _gripperRotationSlider.value = controller.rotationT;
        _elevationToggle.isOn = controller.isLowerPosition;
        _gripToggle.isOn = controller.isGrip;
        
        _rotationBaseSlider.onValueChanged.AddListener(delegate { 
            controller.ChangeBaseRotation(_rotationBaseSlider.value); 
        });
        _armDistanceSlider.onValueChanged.AddListener(delegate
        {
            controller.ChangeDistance(_armDistanceSlider.value);
        });
        _gripperRotationSlider.onValueChanged.AddListener(delegate
        {
            controller.ChangeGripperRotation(_gripperRotationSlider.value);
        });
        _elevationToggle.onValueChanged.AddListener(delegate
        {
            controller.SetGripperElevation(_elevationToggle.isOn);
        });
        _gripToggle.onValueChanged.AddListener(delegate
        {
            controller.Grip(_gripToggle.isOn);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManipulatorPanel : MonoBehaviour
{
    public static ManipulatorPanel instance;
    [SerializeField] private Slider _rotationBaseSlider, _armDistanceSlider, _gripperRotationSlider;
    [SerializeField] private Toggle _elevationToggle, _gripToggle, _connectionToggle;
    [SerializeField] private Button _changeNameBtn;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TextMeshProUGUI[] _motorLabels, _loadLabels, _temperatureValues;
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
        _connectionToggle.onValueChanged.RemoveAllListeners();
        _rotationBaseSlider.value = controller.rotationX;
        _armDistanceSlider.value = controller.distanceY;
        _gripperRotationSlider.value = controller.rotationT;
        _elevationToggle.isOn = controller.isLowerPosition;
        _gripToggle.isOn = controller.isGrip;
        _connectionToggle.isOn = controller._connection.connectionState;
        _changeNameBtn.onClick.RemoveAllListeners();
        _nameInput.text = controller._connection.name;
        
        _rotationBaseSlider.onValueChanged.AddListener(delegate { 
            controller.ChangeBaseRotation(_rotationBaseSlider.value, false); 
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
        _connectionToggle.onValueChanged.AddListener(delegate
        {
            controller._connection.Connect(_connectionToggle.isOn);
        });
        _changeNameBtn.onClick.AddListener(delegate
        {
            controller._connection.SetName(_nameInput.text);
        });
    }
    public void UpdateMotorLabels(int[] motorValues) => UpdateMonitorLabels(motorValues, _motorLabels);
    public void UpdateLoadLabels(int[] loadValues) => UpdateMonitorLabels(loadValues, _loadLabels);
    public void UpdateTemoperatureLabels(int[] tempValues) => UpdateMonitorLabels(tempValues, _temperatureValues);
    private void UpdateMonitorLabels(int[] values, TextMeshProUGUI[] labels)
    {
        for (int i = 0; i < values.Length; i++)
            labels[i].text = values[i].ToString();
    }
}

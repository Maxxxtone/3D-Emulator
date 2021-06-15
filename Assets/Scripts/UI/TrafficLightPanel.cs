using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrafficLightPanel : MonoBehaviour
{
    public static TrafficLightPanel instance;
    [SerializeField] private Toggle[] _lampsCheckbox;
    [SerializeField] private Toggle _connectionToggle;
    [SerializeField] private Button _changeNameBtn;
    [SerializeField] private TMP_InputField _nameInput;
    private void Awake()
    {
        instance = this;
    }
    public void ChangeTrafficLight(TrafficLightController controller)
    {
        for (int i = 0; i < _lampsCheckbox.Length; i++)
        {
            _lampsCheckbox[i].onValueChanged.RemoveAllListeners();
            _lampsCheckbox[i].isOn = controller._lamps[i].isOn;
        }
        //хуй знает че через цикл не работает
        _lampsCheckbox[0].onValueChanged.AddListener(delegate
        {
            controller._lamps[0].SetLampState(_lampsCheckbox[0].isOn);
        });
        _lampsCheckbox[1].onValueChanged.AddListener(delegate
        {
            controller._lamps[1].SetLampState(_lampsCheckbox[1].isOn);
        });
        _lampsCheckbox[2].onValueChanged.AddListener(delegate
        {
            controller._lamps[2].SetLampState(_lampsCheckbox[2].isOn);
        });
        _lampsCheckbox[3].onValueChanged.AddListener(delegate
        {
            controller._lamps[3].SetLampState(_lampsCheckbox[3].isOn);
        });
        _connectionToggle.onValueChanged.AddListener(delegate
        {
            controller.connection.Connect(_connectionToggle.isOn);
        });
        _changeNameBtn.onClick.AddListener(delegate
        {
            controller.connection.SetName(_nameInput.text);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightPanel : MonoBehaviour
{
    public static TrafficLightPanel instance;
    [SerializeField] private Toggle[] _lampsCheckbox;
    private void Awake()
    {
        instance = this;
    }
    public void ChangeTrafficLight(TrafficLightController controller)
    {
        for (int i = 0; i < _lampsCheckbox.Length; i++)
        {
            _lampsCheckbox[i].onValueChanged.RemoveAllListeners();
            print(controller._lamps[i].isOn);
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
    }
}

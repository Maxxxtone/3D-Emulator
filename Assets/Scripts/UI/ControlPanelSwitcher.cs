using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelSwitcher : MonoBehaviour
{
    public static ControlPanelSwitcher instance;
    public GameObject manipulatorPanel, trafficLightPanel;
    private void Awake()
    {
        instance = this;
    }
    //����� �-��� ������������ � ��������� �� ����
    //������ ������ ���������� � ������
    //����� ��� ����������� � ������������ ������ �����
    public void ShowManipulatorPanel()
    {
        manipulatorPanel.SetActive(true);
        trafficLightPanel.SetActive(false);
    }
    public void ShowTrafficLightPanel()
    {
        manipulatorPanel.SetActive(false);
        trafficLightPanel.SetActive(true);
    }
}

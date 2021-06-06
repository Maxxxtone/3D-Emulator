using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelSwitcher : MonoBehaviour
{
    public static ControlPanelSwitcher instance;
    public GameObject manipulatorPanel, trafficLightPanel, remoteTerminalPanel;
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
        remoteTerminalPanel.SetActive(false);
    }
    public void ShowTrafficLightPanel()
    {
        manipulatorPanel.SetActive(false);
        trafficLightPanel.SetActive(true);
        remoteTerminalPanel.SetActive(false);
    }
    public void ShowRemoteTerminalPanel()
    {
        manipulatorPanel.SetActive(false);
        trafficLightPanel.SetActive(false);
        remoteTerminalPanel.SetActive(true);
    }
}

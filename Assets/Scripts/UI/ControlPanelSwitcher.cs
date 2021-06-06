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
    //общая ф-ция переключения с передачей по типу
    //нужная панель кешируется в начале
    //потом все отключаются и кешированный объект эктив
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

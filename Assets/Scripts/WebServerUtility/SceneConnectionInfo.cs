using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConnectionInfo : MonoBehaviour
{
    public static SceneConnectionInfo instance;
    public const string SocketIP = "ws://localhost:4000";
    public string ServerUrl { get; private set; }
    public string ApplicationKey { get; private set; }
    private void Awake()
    {
        instance = this;
        ServerUrl = "https://pp-2105240757me.devportal.ptc.io";//PlayerPrefs.GetString("Url");
        ApplicationKey = "40dc8315-aae6-4b9b-a53a-d3b31ae27508";//PlayerPrefs.GetString("AppKey");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SaveSystem.instance.Save();
        SceneManager.LoadScene(0);
    }
}

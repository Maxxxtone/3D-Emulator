using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThingController
{
    string SetInputsData(int[] inputs);
    Dictionary<string, int> GetOutputData();
    string GetName();
    void SetName(string name);
}

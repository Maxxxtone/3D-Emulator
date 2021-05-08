using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameTransform : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    private void Update()
    {
        transform.position = _targetObject.position;
    }
}

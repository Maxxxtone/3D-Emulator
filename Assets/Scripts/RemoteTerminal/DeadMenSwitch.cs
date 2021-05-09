using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenSwitch : MonoBehaviour
{
    private int _dmsState;
    private Animator _animator;
    private bool _mouseOn;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_mouseOn && Input.GetMouseButtonDown(0))
            Click();
    }
    private void OnMouseEnter()
    {
        _mouseOn = true;
    }
    private void OnMouseExit()
    {
        _mouseOn = false;
    }
    private void Click()
    {
        _dmsState = Mathf.Abs(_dmsState - 1);
        _animator.SetInteger("state",_dmsState);
    }
}

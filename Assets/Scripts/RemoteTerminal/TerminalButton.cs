using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalButton : MonoBehaviour
{
    [SerializeField] private TextMesh _clicksText;
    [SerializeField] private string _buttonName = "b1";
    [SerializeField] private string _displayedText = " ÌÓÔÍ‡π1: ";
    private Animator _animator;
    private int _clicksCount;
    private bool _mouseOn;
    [SerializeField] private RemoteTerminal _terminal;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _clicksText.text = _displayedText + _clicksCount;
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
        _clicksCount++;
        _terminal.SetButtonClicks(_buttonName, _clicksCount);
        _clicksText.text = _displayedText + _clicksCount;
        _animator.SetTrigger("click");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    //private int _hp;
    [SerializeField] private Slider _rotationSlider, _distanceSlider, _gripperRotationSlider;
    [SerializeField] private float distanceY = 200f, rotationX = 200f;
    [SerializeField] private GameObject controlPoint, mainControlPoint;
    [SerializeField] private Transform _gripperArm;
    [SerializeField] private float _lowerPosition = 0.1f;
    [SerializeField] private RobotIKController robot;
    private float _startElevation, _currentElevation;
    private Transform _dragDetail;

    private void Start()
    {
        _currentElevation = _startElevation = controlPoint.transform.localPosition.z;
        controlPoint.transform.localPosition = new Vector3(0, _startElevation, 4.25f);
    }
    private void Update()
    {
        rotationX = Mathf.Clamp(_rotationSlider.value*360,45f,315f);
        distanceY = Mathf.Clamp(-_distanceSlider.value/10000, -.035f, - .01f);
        mainControlPoint.transform.localEulerAngles = new Vector3(0,0, rotationX);
        controlPoint.transform.localPosition = new Vector3(0,distanceY, _currentElevation);
        _gripperArm.localEulerAngles = new Vector3(0, _gripperRotationSlider.value, 0);
    }
    public void SetGripperElevation(bool up)
    {
        var pos = controlPoint.transform.localPosition;
        if (up)
            pos.z = _lowerPosition;
        else
            pos.z = _startElevation;
        _currentElevation = pos.z;
        controlPoint.transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    [SerializeField] private Slider _gripperRotationSlider;
    public float distanceY = 200f, rotationX = 200f, rotationT = 0;
    public bool isLowerPosition, isGrip;
    [SerializeField] private GameObject controlPoint, mainControlPoint;
    [SerializeField] private Transform _gripperArm;
    [SerializeField] private float _lowerPosition = 0.1f;
    [SerializeField] private EndJointRay _gripper;
    private float _startElevation, _currentElevation;
    public RobotConnection _connection;

    private void Start()
    {
        _connection = GetComponent<RobotConnection>();
        _currentElevation = _startElevation = controlPoint.transform.localPosition.z;
        controlPoint.transform.localPosition = new Vector3(0, controlPoint.transform.localPosition.y, _startElevation);
    }
    public void SetGripperElevation(bool up)
    {
        var pos = controlPoint.transform.localPosition;
        if (up)
            pos.z = _lowerPosition;
        else
            pos.z = _startElevation;
        _currentElevation = pos.z;
        isLowerPosition = up;
        controlPoint.transform.localPosition = pos;
    }
    public void SetGripperElevation(int elevationValue)
    {
        var pos = controlPoint.transform.localPosition;
        if (elevationValue == 1)
        {
            pos.z = _lowerPosition;
            isLowerPosition = true;
        }
        else if (elevationValue == 0)
        {
            pos.z = _startElevation;
            isLowerPosition = false;
        }
        _currentElevation = pos.z;
        controlPoint.transform.localPosition = pos;
    }
    public void ChangeBaseRotation(float angle, bool isConnected)
    {
        rotationX = Mathf.Clamp(angle * 360, 45f, 315f);
        if (isConnected)
            rotationX = Mathf.Clamp(angle, 45f, 315f);
        mainControlPoint.transform.localEulerAngles = new Vector3(0, 0, rotationX);
    }
    public void ChangeDistance(float distance)
    {
        distanceY = distance;
        var y = Mathf.Clamp(-distance / 10000, -.035f, -.01f);
        controlPoint.transform.localPosition = new Vector3(0, y, _currentElevation);
    }
    public void ChangeGripperRotation(float angle)
    {
        rotationT = Mathf.Clamp(angle * 360, 45f, 315f);
        _gripperArm.localEulerAngles = new Vector3(0, angle, 0);
    }
    public void Grip(bool gripState)
    {
        isGrip = gripState;
        _gripper.SetGrabState(gripState);
    }
    public void Grip(int gripState)
    {
        if (gripState == 1)
            isGrip = true;
        else if (gripState == 0)
            isGrip = false;
        _gripper.SetGrabState(isGrip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIKController : MonoBehaviour
{
    [SerializeField] private RobotJoint _rootJoint;
    [SerializeField] private RobotJoint _endJoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private EndJointRay _gripperRay;
    [SerializeField] private float rate = 8f;
    [SerializeField] private int _steps = 15;
    private RobotJoint current;
    [SerializeField] private float _threshholdRotate = .35f, _threshholdDistance = .15f;
    private float CalculateSlope(RobotJoint joint)
    {
        float deltaTheta = 0.01f;
        var distance1 = Vector3.Distance(_endJoint.transform.position, _targetPoint.transform.position);
        joint.Rotate(deltaTheta);
        var distance2 = Vector3.Distance(_endJoint.transform.position, _targetPoint.transform.position);
        joint.Rotate(-deltaTheta);
        return (distance1 - distance2) / deltaTheta;
    }
    private void Update()
    {
        MoveRobot();
    }
    public void MoveRobot()
    {
        var distance = Vector3.Distance(_endJoint.transform.position, _targetPoint.transform.position);
        if (distance > _threshholdDistance && distance <= _threshholdRotate)
        {
            for (int i = 0; i < _steps; ++i)
            {
                current = _rootJoint;
                while (current != null)
                {
                    var slope = CalculateSlope(current);
                    current.Rotate(slope * rate);
                    current = current.GetChildJoint();
                }
            }
        }
        else if(distance > _threshholdRotate)
            _rootJoint.Rotate(4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    [SerializeField] private RobotJoint m_childJoint;
    [SerializeField] private float m_minAngle = 0f, m_maxAngle = 90f;
    [SerializeField] private Vector3 m_rotationDirection = Vector3.up;
    [SerializeField] private bool printAngle = false;    

    public RobotJoint GetChildJoint() => m_childJoint;

    public void Rotate(float angle)
    {
        transform.Rotate(m_rotationDirection * angle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    [SerializeField] private RobotJoint m_childJoint;

    public RobotJoint GetChildJoint() => m_childJoint;

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.up * angle);
    }
}

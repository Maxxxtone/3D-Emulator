using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJointRay : MonoBehaviour
{
    [SerializeField] private float m_distanceToFloor = 0.5f;
    [SerializeField] private LayerMask m_floorLayer = 1;
    [SerializeField] private RobotJoint m_joint;
    [SerializeField] private Transform m_targetPoint;
    private const int hitRayLength = 5;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up * hitRayLength);
        Physics.Raycast(ray, out hit, m_floorLayer);
        if(hit.collider != null)
        {
            var angle = IsGripperPerpendicular(transform.up * hitRayLength, hit.collider.transform.right);
            if(angle >= 91f)
            {
                m_joint.Rotate(-.5f * m_targetPoint.position.normalized.x);
            }
            else if(angle <= 89)
            {
                m_joint.Rotate(.5f * m_targetPoint.position.normalized.x);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up * 10);
    }

    private float IsGripperPerpendicular(Vector3 grippedDirectionVector, Vector3 floor)
    {
        return Vector3.Angle(grippedDirectionVector, floor);
    }
}

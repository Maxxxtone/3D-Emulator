using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJointRay : MonoBehaviour
{
    [SerializeField] private float m_distanceToFloor = 0.5f, _grabSphereRadius = 0.25f;
    [SerializeField] private LayerMask m_floorLayer = 1, _draggableItemsLayer = 2;
    [SerializeField] private RobotJoint m_joint;
    [SerializeField] private Transform m_targetPoint;
    private const int hitRayLength = 5;
    private bool _hasItem;
    private Transform _draggableItem;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up * hitRayLength);
        Physics.Raycast(ray, out hit, m_floorLayer);
        if(hit.collider != null)
        {
            var angle = GetGripperAngle(transform.up * hitRayLength, hit.transform.right);
            if (angle >= 91f)
            {
                m_joint.Rotate(-2f * m_targetPoint.position.normalized.x);
            }
            else if (angle <= 89)
            {
                m_joint.Rotate(2f * m_targetPoint.position.normalized.x);
            }
        }
        if (_hasItem)
        {
            _draggableItem.position = transform.position;
            _draggableItem.rotation = transform.rotation;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up * 10);
        Gizmos.DrawSphere(transform.position, _grabSphereRadius);
    }

    private float GetGripperAngle(Vector3 grippedDirectionVector, Vector3 floor)
    {
        return Vector3.Angle(grippedDirectionVector, floor);
    }
    public void SetGrabState(bool isGrab)
    {
        _animator.SetBool("grip", isGrab);
        if (isGrab)
        {
            var draggableItems = Physics.OverlapSphere(transform.position, _grabSphereRadius, _draggableItemsLayer);
            if(draggableItems.Length > 0 && _draggableItem == null)
            {
                if (transform.rotation.y - draggableItems[0].transform.rotation.y < 5f)
                {
                    _hasItem = true;
                    _draggableItem = draggableItems[0].transform;
                    _draggableItem.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        else
        {
            if (_hasItem)
            {
                _draggableItem.GetComponent<Rigidbody>().isKinematic = false;
                _hasItem = false;
                _draggableItem = null;
            }
        }

    }
}

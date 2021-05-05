using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    [SerializeField] private GameObject _objectToMove, _objectToSpawn;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _yValue = 0.44f;
    private bool _collideWithOther;
    private Vector3 oldPos;
    private GameObject _currentMoveObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnObject();
        if (_currentMoveObject != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
            {
                if(!_collideWithOther)
                    oldPos = _currentMoveObject.transform.position;
                var PosX = hit.point.x;
                var PosZ = hit.point.z;
                _currentMoveObject.transform.position = new Vector3(PosX, _yValue, PosZ);
            }
            if (Input.GetMouseButtonDown(0))
            {
                var thing = Instantiate(_objectToSpawn, oldPos, _objectToSpawn.transform.rotation);
                thing.GetComponent<RaycastTarget>().enabled = true;
                Destroy(_currentMoveObject);
            }
        }
    }
    public void SpawnObject()
    {
        _currentMoveObject = Instantiate(_objectToMove, Vector3.zero, _objectToMove.transform.rotation);
    }
}

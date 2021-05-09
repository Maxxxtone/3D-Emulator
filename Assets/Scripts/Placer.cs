using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public static Placer instance;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _yValue = 0.44f;
    private bool _collideWithOther;
    private Vector3 oldPos;
    private GameObject _currentMoveObject;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
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
                var rayTarget = thing.GetComponent<RaycastTarget>();
                rayTarget.enabled = true;
                SaveSystem.instance.things.Add(rayTarget);
                Destroy(_currentMoveObject);
            }
        }
    }
    public void SetObjectToSpawn(GameObject objectToMove, GameObject objectToSpawn)
    {
        _currentMoveObject = Instantiate(objectToMove, Vector3.zero, objectToMove.transform.rotation);
        _objectToSpawn = objectToSpawn;
    }
}

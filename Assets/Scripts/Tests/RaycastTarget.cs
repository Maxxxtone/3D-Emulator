using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
    [SerializeField] private GameObject _selectCircle;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ThingType _type = ThingType.Manipulator;
    [SerializeField] private Vector3 _rotationVector = Vector3.forward;
    private bool _mouseOn;
    private bool _collideWithOther;
    private Vector3 oldPos;
    private float _timer;
    private void Start()
    {
        oldPos = transform.position;
        _timer = 0.1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            ThingSwitcher.instance.DeleteActiveThing();
        if (_mouseOn && Input.GetMouseButtonDown(0))
            ThingSwitcher.instance.SwitchThing(_type, gameObject);
        if (_mouseOn && Input.GetMouseButton(0))
        {
            if (_timer <= 0)
            {
                if (Input.GetKey(KeyCode.E))
                    transform.Rotate(_rotationVector);
                else if (Input.GetKey(KeyCode.Q))
                    transform.Rotate(-_rotationVector);
                _selectCircle.SetActive(true);
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
                {
                    if (!_collideWithOther)
                        oldPos = transform.position;
                    var PosX = hit.point.x;
                    var PosZ = hit.point.z;
                    transform.position = new Vector3(PosX, transform.position.y, PosZ);
                }
                if (Input.GetMouseButtonDown(1))
                    transform.position = new Vector3(0, transform.position.y, 0);
            }
            else if (_timer > -1)
                _timer -= Time.deltaTime;
        }
        else
        {
            transform.position = oldPos;
            _selectCircle.SetActive(false);
        }
    }
    private void OnMouseEnter()
    {
        _timer = 0.1f;
        _mouseOn = true;
    }
    private void OnMouseExit()
    {
        _mouseOn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            float x = 1.25f;
            if (other.transform.position.x < transform.position.x)
                x = -1.25f;
            transform.position = transform.position - new Vector3(x, 0, 1f);
            oldPos = transform.position;
            _collideWithOther = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
            _collideWithOther = false;
    }
}

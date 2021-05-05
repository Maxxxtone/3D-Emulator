using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
    [SerializeField] private GameObject _selectCircle;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ThingType _type = ThingType.Manipulator;
    private bool _mouseOn;
    private bool _collideWithOther;
    private Vector3 oldPos;
    private void Start()
    {
        oldPos = transform.position;
    }
    private void Update()
    {
        if (_mouseOn && Input.GetMouseButtonDown(0))
            ThingSwitcher.instance.SwitchThing(_type, gameObject);
        if (_mouseOn && Input.GetMouseButton(0))
        {
            //тут можно логику свитчера ебануть
            //для таргета добавить поле тип оборудования
            //обновление ui при выборе
            _selectCircle.SetActive(true);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
            {
                if(!_collideWithOther)
                    oldPos = transform.position;
                var PosX = hit.point.x;
                var PosZ = hit.point.z;
                transform.position = new Vector3(PosX, transform.position.y, PosZ);
            }
            if (Input.GetMouseButtonDown(1))
                transform.position = new Vector3(0, transform.position.y, 0);
        }
        else
        {
            transform.position = oldPos;
            _selectCircle.SetActive(false);
        }
    }
    private void OnMouseEnter()
    {
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

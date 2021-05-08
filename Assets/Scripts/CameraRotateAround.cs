using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
	public float turnSpeed = 4.0f;
	public float panSpeed = 4.0f;
	public float zoomSpeed = 4.0f;
	[SerializeField] private Vector3 _originalPosition = Vector3.one;
	[SerializeField] private float _xConstraintRight, _xConstraintLeft, _zBackConstraint, _zForwardConstraint,
		_yConstraintDown, _yConstraintUp; 

	private Vector3 mouseOrigin;
	private bool _isPanning;     
	private bool _isRotating;
	private bool _isZooming;
	private float _zoomCoef = 1;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			transform.position = _originalPosition;
		if (Input.GetMouseButtonDown(1))
		{
			mouseOrigin = Input.mousePosition;
			_isRotating = true;
		}
		if (Input.GetMouseButtonDown(2))
		{
			mouseOrigin = Input.mousePosition;
			_isPanning = true;
		}

		_zoomCoef = (Input.GetAxis("Mouse ScrollWheel"));

		if (!Input.GetMouseButton(1)) _isRotating = false;
		if (!Input.GetMouseButton(2)) _isPanning = false;

		if (Input.GetAxis("Mouse ScrollWheel") == 0) _isZooming = false;
		else _isZooming = true;

		if (_isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		if (_isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = new Vector3(pos.x * -panSpeed, pos.y * -panSpeed, 0);
			transform.Translate(move, Space.Self);
		}
		if (_isZooming)
			Zoom(/*Input.GetAxis("Mouse ScrollWheel")*/);
	}
    private void LateUpdate()
    {
		var posCam = transform.position;
		posCam.x = Mathf.Clamp(transform.position.x, _xConstraintLeft, _xConstraintRight);
		posCam.z = Mathf.Clamp(transform.position.z, _zBackConstraint, _zForwardConstraint);
		posCam.y = Mathf.Clamp(transform.position.y, _yConstraintDown, _yConstraintUp);
		transform.position = posCam;
	}
    private void Zoom(/*float zoomCoefficient*/)
    {
		//Vector3 targetPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
		Vector3 move = zoomSpeed * _zoomCoef * Vector3.forward;
		transform.Translate(move, Space.Self);
    }
}

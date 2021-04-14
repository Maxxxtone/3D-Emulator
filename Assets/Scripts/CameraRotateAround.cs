using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
	public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;       // Speed of the camera when being panned
	public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth

	private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
	private bool isPanning;     // Is the camera being panned?
	private bool isRotating;    // Is the camera being rotated?
	private bool isZooming;     // Is the camera zooming?
	private float zoomCoef = 1;

	void Update()
	{
		// Get the left mouse button
		if (Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		// Get the right mouse button
		if (Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}

		zoomCoef = (Input.GetAxis("Mouse ScrollWheel"));

		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating = false;
		if (!Input.GetMouseButton(2)) isPanning = false;

		if (Input.GetAxis("Mouse ScrollWheel") == 0) isZooming = false;
		else isZooming = true;

		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = new Vector3(pos.x * -panSpeed, pos.y * -panSpeed, 0);
			transform.Translate(move, Space.Self);
		}
		if (isZooming)
		{
			Zoom(Input.GetAxis("Mouse ScrollWheel"));
		}
	}
	private void Zoom(float zoomCoefficient)
    {
		Vector3 targetPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
		Vector3 move = zoomSpeed * zoomCoef * Vector3.forward;
		transform.Translate(move, Space.Self);
    }
}

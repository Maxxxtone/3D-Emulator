using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    [SerializeField] private Slider _xSlider, _ySlider;
    [SerializeField] private float distanceY = 200f, rotationX = 200f;
    [SerializeField] private GameObject controlPoint, mainControlPoint;
    private float startY;
    [SerializeField] private RobotIKController robot;

    private void Start()
    {
        startY = controlPoint.transform.localPosition.y;
        controlPoint.transform.localPosition = new Vector3(0, startY, 4.25f);
    }
    private void Update()
    {
        rotationX = Mathf.Clamp(_xSlider.value*360,_xSlider.value * 45f,_xSlider.value * 315f);
        distanceY = Mathf.Clamp(_ySlider.value/70f, 2.4f, 4.25f);
        mainControlPoint.transform.localEulerAngles = new Vector3(0,rotationX,0);
        controlPoint.transform.localPosition = new Vector3(0,startY, distanceY);
    }
}

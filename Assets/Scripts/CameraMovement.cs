using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _positionX;
    private float _positionY;

    private const string MouseXAxis= "MouseX";
    private const string MouseYAxis= "MouseY";

    private void Update()
    {
        float mouseXPosition = Input.GetAxis(MouseXAxis);
        float mouseYPosition = Input.GetAxis(MouseYAxis);
        _positionX = 0;
        _positionY = 0;
        if (mouseXPosition != 0)
        {
            _positionX += mouseXPosition * _rotationSpeed;
            transform.Rotate(Vector3.up, _positionX);
        }
        if (mouseYPosition != 0)
        {
            _positionY -= mouseYPosition * _rotationSpeed;
            transform.Rotate(Vector3.right, _positionY);
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,0);
    }
}

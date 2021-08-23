using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _cubeMaxPositionX;
    [Space]
    [SerializeField] private TouchSlider _touchSlider;
    [SerializeField] private Cube _mainCube;

    private bool _isPointerDown;
    private Vector3 _cubePostion;

    private void Start()
    {
        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
        _touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (_isPointerDown)
            _mainCube.transform.position = Vector3.Lerp(
               _mainCube.transform.position,
               _cubePostion,
               _moveSpeed * Time.deltaTime
            );
    }

    private void OnPointerUp()
    {
        _isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (_isPointerDown)
        {
            _cubePostion = _mainCube.transform.position;
            _cubePostion.x = xMovement * _cubeMaxPositionX;
        }
    }

    private void OnPointerDown()
    {
        if (_isPointerDown)
        {
            _isPointerDown = false;

            //push the cube
            _mainCube.Rigidbody.AddForce(Vector3.forward * _pushForce, ForceMode.Impulse);

            //spawn cube

        }
    }

    private void OnDestroy()
    {
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
        _touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}


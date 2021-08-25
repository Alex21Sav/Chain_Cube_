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
    private Cube _mainCube;

    private bool _isPointerDown;
    private bool _canMove;
    private Vector3 _cubePostion;

    private void Start()
    {
        SpawnCube();
        _canMove = true; 

        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
        _touchSlider.OnPointerUpEvent += OnPointerUp;
    }
    private void Update()
    {
        if (_isPointerDown)
            _mainCube.transform.position = Vector3.Lerp(_mainCube.transform.position, _cubePostion, _moveSpeed * Time.deltaTime);
    }
    private void OnPointerDown()
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
    private void OnPointerUp()
    {
        if (_isPointerDown && _canMove)
        {
            _isPointerDown = false;
            _canMove = false;
            //push the cube
            _mainCube.CubeRigidbody.AddForce(Vector3.forward * _pushForce, ForceMode.Impulse);

            //spawn cube
            Invoke ("SpawnNewCube", 0.3f);
        }
    }
    private void SpawnNewCube()
    {
        _mainCube.IsMainCube = false;
        _canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        _mainCube = CubeSpawner.Instance.SpawnRandom();
        _mainCube.IsMainCube = true;
        _cubePostion = _mainCube.transform.position;
    }
    private void OnDestroy()
    {
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
        _touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}


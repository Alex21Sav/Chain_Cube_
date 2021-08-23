using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public bool IsMainCube;

    private MeshRenderer _cubeMeshRenderer;

    private void Awake()
    {
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        CubeColor = color;
        _cubeMeshRenderer.material.color = color;
    }

    public void SetNumbe(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            _numbersText[i].text = number.ToString();
        }
    }

}

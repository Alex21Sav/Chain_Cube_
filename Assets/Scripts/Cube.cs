using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticID = 0;

    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public int CubeID;
    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidbody;
    [HideInInspector] public bool IsMainCube;

    private MeshRenderer _cubeMeshRenderer;

    private void Awake()
    {
        CubeID = staticID++;
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
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

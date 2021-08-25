using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;

    Queue<Cube> CubesQueue = new Queue<Cube>();

    [SerializeField] private int _cubesQueueCapacity = 20;
    [SerializeField] private bool _autoQueueGrow = true;
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private Color[] _cubeColors;

    [HideInInspector] public int maxCubeNumber;
    //in our case it's 4096 2^12
    private int _maxPower = 12;
    private Vector3 _defaultSpawnPosition;     

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, _maxPower);

        InitializeCubesQueue();
    }
    private void InitializeCubesQueue()
    {
        for (int i = 0; i < _cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(_prefabCube, _defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();

        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        CubesQueue.Enqueue(cube);

    }
    public Cube Spawn(int number, Vector3 positin)
    {
        if (CubesQueue.Count == 0)
        {
            if (_autoQueueGrow)
            {
                _cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Cubes Queue] = no more cubes available in the pool");
                return null;
            }
        }
        Cube cube = CubesQueue.Dequeue();
        cube.transform.position = positin;
        cube.SetNumbe(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }
    public Cube SpawnRandom()
    {
        return Spawn(GenrateRandomNumber(), _defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        CubesQueue.Enqueue(cube);
    }
    public int GenrateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int nomber)
    {
        return _cubeColors[(int)(Mathf.Log(nomber) / Mathf.Log(2)) - 1 ];
    }
}

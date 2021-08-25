using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cubeExplosionFX;

    ParticleSystem.MainModule _cubeExplosionFXModule;

    public static FX Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _cubeExplosionFXModule = _cubeExplosionFX.main;
    }
    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        _cubeExplosionFXModule.startColor = new ParticleSystem.MinMaxGradient(color);

        _cubeExplosionFX.transform.position = position;

        _cubeExplosionFX.Play();
    }
}

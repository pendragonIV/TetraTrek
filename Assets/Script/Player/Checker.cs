using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public static Checker instance;
    [SerializeField] private Transform _cubeContainer;
    private int _cubeCount;
    private int _currentCubeCount;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        _cubeCount = _cubeContainer.childCount;
    }

    public void AddCube()
    {
        _currentCubeCount++;
        if(_currentCubeCount == _cubeCount)
        {
            GameManager.instance.Win();
        }
    }

    public void DecreaseCubeCount()
    {
        _currentCubeCount--;
    }
}

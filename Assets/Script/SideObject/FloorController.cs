using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private Material _floorMaterial;
    [SerializeField] private Color[] _floorColors;
    [SerializeField] private float _colorChangeDuration;
    private float _targetPoint;

    private void Start()
    {
        StartCoroutine(ChangeFloorColor());
    }


    private IEnumerator ChangeFloorColor()
    {
        while (true)
        {
            Color newColor = _floorColors[UnityEngine.Random.Range(0, _floorColors.Length)];
            _targetPoint = 0;
            while(_targetPoint < 1)
            {
                _targetPoint += Time.deltaTime / _colorChangeDuration;
                _floorMaterial.color = Color.Lerp(_floorMaterial.color, newColor, _targetPoint);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private int _maxLines;
    [SerializeField] private float _lineZoomSpeed;
    [SerializeField] private float _lineZoomTime;
    List<GameObject> _currentLines = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _maxLines; i++)
        {
            InitNewOutterLineOfDestination();
        }

        for (int i = 0; i < _currentLines.Count; i++)
        {
            ZoomOutterIn(_currentLines[i].transform, i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            other.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            Checker.instance.AddCube();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            other.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
            Checker.instance.DecreaseCubeCount();
        }
    }

    private void ZoomOutterIn(Transform outer, int delayLevel)
    {
        Vector3 zoomDestination = new Vector3(0, outer.transform.localScale.y, 0);
        outer.transform.DOScale(zoomDestination, _lineZoomTime).SetDelay(_lineZoomSpeed * delayLevel).OnComplete(() =>
        {
            GameObject newOuterLine = InitNewOutterLineOfDestination();
            ZoomOutterIn(newOuterLine.transform, _currentLines.Count - 1);
            outer.DOKill();
            Destroy(outer.gameObject);
        });
    }

    private GameObject InitNewOutterLineOfDestination()
    {
        GameObject line = Instantiate(_linePrefab, transform);
        _currentLines.Add(line);
        return line;
    }
}

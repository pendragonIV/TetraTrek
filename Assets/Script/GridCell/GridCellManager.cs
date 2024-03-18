using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager instance;

    [SerializeField]
    private Tilemap tileMap;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool IsObjectAtCell(Vector3Int cellPosition)
    {
        Vector3 pos = tileMap.GetCellCenterWorld(cellPosition);
        if (Physics.OverlapSphere(pos, 0.1f).Length > 0)
        {
            return true;
        }
        return false;
    }

    public Vector3Int GetCellPosOfGivenWorldPos(Vector3 position)
    {
        Vector3Int cellPosition = tileMap.WorldToCell(position);
        return cellPosition;
    }

    public Vector3 GetWordPosOfGivenCellPos(Vector3Int cellPosition)
    {
        return tileMap.GetCellCenterWorld(cellPosition);
    }
}

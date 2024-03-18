using UnityEngine;

public class CenterCell : MonoBehaviour
{
    private void Start()
    {
        PutObjectToCenterCell();
    }
    public void PutObjectToCenterCell()
    {
        Vector3Int cellPos = GridCellManager.instance.GetCellPosOfGivenWorldPos(transform.position);
        Vector3 pos = GridCellManager.instance.GetWordPosOfGivenCellPos(cellPos);
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    #region movement
    [SerializeField] private Vector3 _moveDirection;
    private bool _isMoving = false;
    #endregion

    private void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    private void Update()
    {
        if(_moveDirection != Vector3.zero)
        {
            MovePlayerCubeController();
        }
    }

    #region movement

    private void MovePlayerCubeController()
    {
        if(GameManager.instance.IsGameWin() || GameManager.instance.IsGameLose() || GameManager.instance.IsGamePause()) return;
        if (_isMoving) return;
        _isMoving = true;
        Vector3Int currentCellPos = GridCellManager.instance.GetCellPosOfGivenWorldPos(transform.position);
        Vector3Int nextCellPos = currentCellPos + Vector3Int.FloorToInt(_moveDirection);
        if (!GridCellManager.instance.IsObjectAtCell(nextCellPos))
        {
            _isMoving = false;
            return;
        }
        GameObject pushableCube = CheckNextPushableCube(nextCellPos);
        if (pushableCube != null)
        {
            if(!MovePushableCube(pushableCube))
            {
                _isMoving = false;
                return;
            }
        }
        MoveCube(this.gameObject , nextCellPos, currentCellPos);
    }

    private void MoveCube(GameObject cubeToMove, Vector3Int nextCellPos, Vector3Int currentCellPos)
    {
        Vector3 nextCellWorldPos = GridCellManager.instance.GetWordPosOfGivenCellPos(nextCellPos);
        Vector3 posToMove = new Vector3(nextCellWorldPos.x, cubeToMove.transform.position.y, nextCellWorldPos.z);
        cubeToMove.transform.DOMove(posToMove, 0.3f).OnComplete(() => _isMoving = false);
        RotateCubeWhenMove(cubeToMove, GetAngleFromDirection(currentCellPos, nextCellPos));
    }

    private bool MovePushableCube(GameObject cube)
    {
        Vector3Int currentCellPos = GridCellManager.instance.GetCellPosOfGivenWorldPos(cube.transform.position);
        Vector3Int nextCellPos = currentCellPos + Vector3Int.FloorToInt(_moveDirection);
        if (!GridCellManager.instance.IsObjectAtCell(nextCellPos))
        {
            return false;
        }
        GameObject pushableCube = CheckNextPushableCube(nextCellPos);
        if (pushableCube != null)
        {
            return false;
        }
        MoveCube(cube, nextCellPos, currentCellPos);
        return true;
    }

    #endregion

    #region check

    private GameObject CheckNextPushableCube(Vector3Int cellPosition)
    {
        Vector3 pos = GridCellManager.instance.GetWordPosOfGivenCellPos(cellPosition);
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, LayerMask.GetMask("Pushable"));
        if (colliders.Length > 0)
        {
            return colliders[0].gameObject;
        }
        return null;
    }

    #endregion

    #region rotation
    private void RotateCubeWhenMove(GameObject cubeToRotate, Vector3 angle)
    {
        cubeToRotate.transform.DORotate(cubeToRotate.transform.rotation.eulerAngles + angle, 0.3f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            cubeToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        });
    }

    private Vector3 GetAngleFromDirection(Vector3Int from, Vector3Int to)
    {
        Vector3Int direction = to - from;
        if (direction == Vector3Int.up) return new Vector3(90, 0, 0);
        if (direction == Vector3Int.right) return new Vector3(0, 0, -90);
        if (direction == Vector3Int.down) return new Vector3(-90, 0, 0);
        if (direction == Vector3Int.left) return new Vector3(0, 0, 90);
        return Vector3.zero;
    }
    #endregion
}

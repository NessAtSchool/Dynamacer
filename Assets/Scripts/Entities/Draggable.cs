using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{

    private Vector3 _dragOffset;
    private Camera _cam;
    private Transform originalParent;

    private float snapRange = 0.5f;

    void Awake()
    {
        _cam = Camera.main;
        originalParent = transform.parent;
    }

    void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos(transform);
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos(transform) + _dragOffset;
    }


    void OnMouseUp() 
    {
        float closestDistance = -1;
        Transform closestTile = null;
        foreach (Transform tile in GridManager.Instance.Tiles)
        {
            float currentDistance = Vector3.Distance(transform.position, tile.position);
            if (closestTile == null || currentDistance < closestDistance)
            {
                closestTile = tile;
                closestDistance = currentDistance;
            }
        }

        if (closestTile != null && closestDistance <= snapRange)
        {
            transform.SetParent(closestTile);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            transform.SetParent(originalParent);
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }


    Vector3 GetMousePos(Transform transform)
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;

    }



}





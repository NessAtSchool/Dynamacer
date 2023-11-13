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
        transform.parent = null;
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos(transform) + _dragOffset;
    }


    void OnMouseUp()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform tile in GridManager.Instance.Tiles)
        {
            float dist = Vector3.Distance(tile.position, currentPos);
            if (dist < minDist)
            {
                tMin = tile;
                minDist = dist;
            }
        }
        transform.SetParent(tMin);
        transform.localPosition = new Vector3(0, 1, 0);
    }


    Vector3 GetMousePos(Transform transform)
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;

    }



}





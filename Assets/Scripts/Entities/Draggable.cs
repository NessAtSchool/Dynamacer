using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{

    private Vector3 _dragOffset;
    private Camera _cam;
    private Transform originalParent;
    private Transform newParent;
    private Transform oldParent;

    private Transform tMin = null;
    private float minDist;
    private Vector3 currentPos;

    private float snapRange = 4.5f;

    void Awake()
    {
        _cam = Camera.main;
        originalParent = transform.parent;
    }

    void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos(transform);
        transform.SetParent(null);
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos(transform) + _dragOffset;
    }

    private void OnMouseExit()
    {
        foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
        {
            tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = false;
        }
    }


    void OnMouseUp()
    {
        tMin = null;
        minDist = Mathf.Infinity;
        currentPos = transform.position;
        foreach (Transform tile in GridManager.Instance.Tiles)
        {
            float dist = Vector3.Distance(tile.position, currentPos);
            if (dist < minDist)
            {
                tMin = tile;
                minDist = dist;
            }
        }

       //FIX GLITCH THAT BOMB GOES TO EDGE WHEN CLICKED

        //Add snap range back to it goes t original parents of none of the tiles are close enough
        if (tMin.GetComponent<Tile>().AmIOccupied() == true)
        {
            transform.SetParent(originalParent);
        }
        else if (minDist >= snapRange)
        {
            transform.SetParent(originalParent);
        }
        else
        {
            if (tMin != oldParent)
            {
                foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
                {
                    tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = false;
                }

                if (transform.GetComponent<Bomb>() != null)
                {
                    transform.SetParent(tMin);
                    transform.GetComponent<Bomb>().HighlightBobShape(tMin);
                    Debug.Log("highlighed shpae");
                }
            }
            else
            {
                foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
                {
                    tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = false;
                }
            }

            transform.localPosition = new Vector3(0, 1, 0);
            oldParent = tMin;
        }

      


    }


    Vector3 GetMousePos(Transform transform)
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;

    }



}





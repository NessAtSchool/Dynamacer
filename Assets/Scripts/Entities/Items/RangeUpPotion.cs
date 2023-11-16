using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpPotion : PowerUpPotion, IDraggable
{
    [SerializeField] private int _rangeUpAmount = 1;

    //TO MAKE IT SO THAT BOTTLE ONLY DISSEAPPEARS IF YOU LET GO OIF IT ON THE BOMB
    public override void OnTriggerEnter(Collider collision)
    {

        if (gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.GetComponentInParent<Bomb>() != null)
        {
            Bomb bomb = collision.gameObject.GetComponentInParent<Bomb>();
            bomb.ModifyRange(_rangeUpAmount);

            bomb.GetComponentInChildren<SpriteRenderer>().color = _powerUpColor;

            gameObject.SetActive(false);
        }
    }


    private Vector3 _dragOffset;
    private Camera _cam;
    private Transform _originalParent;

    public Vector3 DragOffset { get { return _dragOffset; } private set { _dragOffset = value; } }
    public Camera Cam { get { return _cam; } private set { _cam = value; } }
    public Transform OriginalParent { get { return _originalParent; } private set { _originalParent = value; } }

    public void Awake()
    {
        _cam = Camera.main;
        _originalParent = transform.parent;
    }

    public void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos(transform);
    }

    public void OnMouseDrag()
    {
        transform.SetParent(null);
        transform.position = GetMousePos(transform) + _dragOffset;
    }


    public void OnMouseUp()
    {
        if (transform != null)
        {
            transform.SetParent(_originalParent);
            transform.localPosition = new Vector3(0, 0, -250);
        }

    }

    public Vector3 GetMousePos(Transform transform)
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;
    }

}

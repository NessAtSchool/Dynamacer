using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour,IDraggable, IDamageable
{
    public List<Tile> AffectedArea;
    public int _range = 2;
    private int _baseDamage = 1;
    private int _health = 1;
    private bool _isDestroyed = false;
    private GameObject DeathParticleSystemPrefab;

    public GameObject FireExplosionPrefab;
    public GameObject WaterExplosionPrefab;
    public GameObject StandardExplosionPrefab;

    public Transform ExplosionPosition;

    [SerializeField] private ElementType _element = ElementType.None;

    private List<Transform> thingsBombsCanBeOn = new List<Transform>();



    public ElementType Element { get { return _element; } private set { _element = value; } }
    public int Range { get { return _range; } private set { _range = value; } }
    public int Damage { get { return _baseDamage; } private set { _baseDamage = value; } }
    public int Health { get { return _health; } private set { _health = value; } }
    public bool IsDestroyed { get { return _isDestroyed; } private set { _isDestroyed = value; } }

    private void Start()
    {
        ModifyElement(_element);

        thingsBombsCanBeOn.Add(GridManager.Instance._inventory.transform);
     
        foreach (Transform tile in GridManager.Instance.Tiles)
        {
          thingsBombsCanBeOn.Add(tile);
        }


    }


    public virtual void HighlightBombShape(Transform parentTile)
    {
        return;
    }

    public virtual void Detonate(Bomb bomb)
    {

        foreach (Tile tile in AffectedArea)
        {
            foreach (Transform target in tile.transform)
            {
                if (target.GetComponentInChildren<IDamageable>() != null)
                {
                    target.GetComponent<IDamageable>().TakeDamage(GetDamageDealt(_baseDamage), bomb);
                    Destroy(bomb);
                }
                else
                {
                    Debug.LogWarning("Target does not implement IDamageable.");
                }
            }
        }
    }



    public void ImmuneToBomb(Bomb bomb)
    {
        return;
    }
 

    public void TakeDamage(int amountOfDamage, Bomb originalBomb)
    {
        _health -= amountOfDamage;

        if (_health <= 0)
        {
            _isDestroyed = true;

            if (DeathParticleSystemPrefab.transform != null)
            {
                DeathParticleSystemPrefab.transform.localScale = ExplosionPosition.localScale;
                Instantiate(DeathParticleSystemPrefab, ExplosionPosition.position, ExplosionPosition.rotation);
            }

            foreach (Renderer thing in transform.GetComponentsInChildren<Renderer>())
            {
                Destroy(thing);
            }

            Destroy(transform.gameObject);

           
          
        }

    }

    public void ModifyRange(int mod)
    {
        _range += mod;
        HighlightBombShape(transform.parent.transform);
    }

    public void ModifyElement(ElementType element)
    {
        _element = element;

        switch (_element)
        {
            case ElementType.None:
                DeathParticleSystemPrefab = StandardExplosionPrefab;
                break;
            case ElementType.Water:
                DeathParticleSystemPrefab = WaterExplosionPrefab;
                break;
            case ElementType.Fire:
                DeathParticleSystemPrefab = FireExplosionPrefab;
                break;

            default:
                DeathParticleSystemPrefab = StandardExplosionPrefab;
                break;
        }
       
    }

    public int GetDamageDealt(int baseDamage)
    {
        return baseDamage;
    }


    private Vector3 _dragOffset;
    private Camera _cam;
    private Transform _originalParent;
    private Transform _oldParent;
    private Transform _tMin = null;
    private float _minDist;
    private Vector3 _currentPos;
    private float _snapRange = 8f;
    private bool isDragging = false;


    public Vector3 DragOffset { get { return _dragOffset; } private set { _dragOffset = value; } }
    public Camera Cam { get { return _cam; } private set { _cam = value; } }
    public Transform OriginalParent {get { return _originalParent; } private set { _originalParent = value; } }
    public Transform OldParent { get { return _oldParent; } private set { _oldParent = value; } }
    public Transform TMin { get { return _tMin; } private set { _tMin = value; } }
    public float MinDist { get { return _minDist; } private set { _minDist = value; } }
    public Vector3 CurrentPos { get { return _currentPos; } private set { _currentPos = value; } }
    public float SnapRange { get { return _snapRange; } private set { _snapRange = value; } }


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
        isDragging = true;
        transform.SetParent(null);
        transform.position = GetMousePos(transform) + _dragOffset;
    }

    public void OnMouseExit()
    {
        isDragging = false;
        if (gameObject.GetComponent<Bomb>() != null)
        {
            foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
            {
                tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = false;
            }
        }


        if (gameObject.transform.parent == GridManager.Instance._inventory.transform)
        {
            AffectedArea.Clear();
        }

    }

    public void OnMouseUp()
    {
        if (transform != null)
        {
            _tMin = null;
            _minDist = Mathf.Infinity;
            _currentPos = transform.position;

            if (gameObject.GetComponent<Bomb>() != null)
            {
                
                foreach (Transform tile in thingsBombsCanBeOn)
                {
                    float dist = Vector3.Distance(tile.position, _currentPos);
                    if (dist < _minDist)
                    {
                        _tMin = tile;
                        _minDist = dist;
                    }
                }

                //FIX GLITCH THAT BOMB GOES TO EDGE WHEN CLICKED
                //Add feature that bomb can return to inventory, maybe by tagging it as tile idk yet!

                if (_tMin.GetComponent<Tile>() == null)
                {
                    transform.SetParent(_originalParent);
                    transform.position = Vector3.zero;
                } 
                else if (_tMin.GetComponent<Tile>().AmIOccupied() == true)
                {
                    transform.SetParent(_originalParent);
                    transform.position = Vector3.zero;
                }

                else if (_minDist >= _snapRange)
                {
                    transform.SetParent(_originalParent);
                    transform.position = Vector3.zero;
                }
                else
                {
                    if (_tMin != _oldParent && _tMin.GetComponent<WaterTile>() == null)
                    {
                        foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
                        {
                            tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = false;
                        }

                        if (transform.GetComponent<Bomb>() != null)
                        {
                            transform.SetParent(_tMin);
                            transform.GetComponent<Bomb>().HighlightBombShape(_tMin);

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
                    _oldParent = _tMin;
                }

              
          

            }

            else
            {
                transform.SetParent(GridManager.Instance._inventory.transform);
                transform.localPosition = new Vector3(0, 0, -250);
            }


        }

        if (transform.parent != null)
        {
            if (transform.parent.CompareTag("Canvas"))
            {
                transform.SetParent(_originalParent);
            }
        }

        return;
    }

    public Vector3 GetMousePos(Transform transform)
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        return mousePos;
    }

    public void OnMouseOver()
    {
        if (gameObject.GetComponent<Bomb>() != null)
        {
            foreach (Tile tile in transform.GetComponent<Bomb>().AffectedArea)
            {
                tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = true;
            }
        }
    }
}

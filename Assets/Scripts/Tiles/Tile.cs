using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected Material _baseMaterial;
    [SerializeField] protected MeshRenderer _renderer;
    [SerializeField] private HashSet<ElementType> _detonatedBombTypes = new HashSet<ElementType>();

    public HashSet<ElementType> DetonatedBombTypes { get { return _detonatedBombTypes; } private set { _detonatedBombTypes = value; } }
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    public virtual void Init(int x, int y)
    {
        _renderer.material = _baseMaterial;
    }

    void LateUpdate()
    {
        if (this != null)
        {
            // Highlight
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
            {
                highlight = raycastHit.transform;
                if (highlight.CompareTag("Tile") && highlight != selection)
                {
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }

            // Selection
            if (Input.GetMouseButtonDown(0))
            {
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    selection = raycastHit.transform;
                    selection.gameObject.GetComponent<Outline>().enabled = true;
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        selection = null;
                    }
                }
            }
        }
        return;
    }

   
    private void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.CompareTag("Bomb"))
        {

           gameObject.GetComponent<Outline>().enabled = true;

        }
        else
        {
           gameObject.GetComponent<Outline>().enabled = false;
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public bool AmIOccupied()
    {
        if (transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator HandleBombDetonation(ElementType bombType)
    {
            _detonatedBombTypes.Add(bombType);

            // Check if both types of bombs have detonated
            if (_detonatedBombTypes.Count == 2)
            {
                foreach (ElementType item in _detonatedBombTypes)
                {
                    if (GetComponentInChildren<Building>() != null)
                    {
                        GetComponentInChildren<Building>().MakeImmuneTo(item);
                    }
                }
   
               
            }

            yield return new WaitForSeconds(1);
            _detonatedBombTypes.Clear();
      
    }

    public void MakeImmunetoBomb(Bomb bomb)
    {
        if (GetComponentInChildren<Building>() != null)
        {
            GetComponentInChildren<Building>().ImmuneToBomb(bomb);
        }
    }

}
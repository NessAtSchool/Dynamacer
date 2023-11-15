using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Draggable, IDamageable
{
    public List<Tile> AffectedArea;
    public int _range = 2;
    private int _baseDamage = 1;
    private int _health = 1;
    private bool _isDestroyed = false;
    public GameObject DeathParticleSystemPrefab;
    public Transform ExplosionPosition;

    public int Range { get { return _range; } private set { _range = value; } }
    public int Damage { get { return _baseDamage; } private set { _baseDamage = value; } }
    public int Health { get { return _health; } private set { _health = value; } }
    public bool IsDestroyed { get { return _isDestroyed; } private set { _isDestroyed = value; } }




    //private void LateUpdate()
    //{
    //    return;
    //}

    public virtual void HighlightBobShape( Transform parentTile)
    {
        return;
    }

    public virtual void Detenate()
    {
        //TODO ADD FANCY EFFECTS FOR THE EXPLOSIONS ASWELL AS MUSIC
        
        foreach (Tile tile in AffectedArea)
        {
            foreach (Transform target in tile.transform)
            {
                if (target.GetComponentInChildren<IDamageable>() != null)
                {
                    target.GetComponent<IDamageable>().TakeDamage(GetDamageDealt(_baseDamage));
                }
                else
                {
                    Debug.LogWarning("Target does not implement IDamageable.");
                }
            }
        }
    }

    public void TakeDamage(int amountOfDamage)
    {
        _health -= amountOfDamage;

        if (_health <= 0)
        {
            _isDestroyed = true;

            transform.gameObject.GetComponent<Renderer>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            DeathParticleSystemPrefab.transform.localScale = ExplosionPosition.localScale;
            Instantiate(DeathParticleSystemPrefab, ExplosionPosition.position, ExplosionPosition.rotation);
           
        }

    }

    public void ModifyRange(int mod)
    {
        _range += mod;
        HighlightBobShape(transform.parent.transform);
    }

    public int GetDamageDealt(int baseDamage)
    {
        return baseDamage;
    }
}

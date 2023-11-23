using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour, IDamageable
{
    ElementType _element;
    int _health;
    bool _isDestroyed = false;
    public ElementType Element { get { return _element; } private set { _element = value; } }
    public int Health { get { return _health; } private set { _health = value; } }
    public bool IsDestroyed { get { return _isDestroyed; } private set { _isDestroyed = value; } }

    public void TakeDamage(int amountOfDamage, Bomb originalBomb)
    {

    }

    public void ImmuneToBomb(Bomb bomb)
    {

    }
}

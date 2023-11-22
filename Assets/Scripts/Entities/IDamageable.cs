using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health { get; }
    bool IsDestroyed { get; }
   ElementType Element { get; }

    public void TakeDamage(int amountOfDamage, ElementType elementype);

}

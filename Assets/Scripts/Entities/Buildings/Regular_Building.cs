using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Building : Building
{
    public override void TakeDamage(int amountOfDamage, Bomb bombOrigin)
    {
        foreach (Bomb bomb in immuneToTheseBombs)
        {
            if (bomb == bombOrigin)
            {
                amountOfDamage = 0;
            }
        }

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
}


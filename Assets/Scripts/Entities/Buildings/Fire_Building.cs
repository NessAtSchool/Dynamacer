using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Building : Building
{
    public override void TakeDamage(int amountOfDamage, Bomb bombOrigin)
    {
        _immuneToFire = true;

        foreach (Bomb bomb in immuneToTheseBombs)
        {
            if (bomb == bombOrigin)
            {
                amountOfDamage = 0;
            }

            print(bomb.gameObject.name);
        }


        if (bombOrigin.Element == ElementType.Water && _immuneToWater == false ||
                         bombOrigin.Element == ElementType.Fire && _immuneToFire == false ||
                         bombOrigin.Element == ElementType.None)
        {
            _health -= amountOfDamage;
            Debug.Log("Taking damage: " + amountOfDamage);
        }


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

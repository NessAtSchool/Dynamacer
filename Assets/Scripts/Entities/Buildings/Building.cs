using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    private int _health = 1;
    [SerializeField] private bool _isDestroyed = false;
    public GameObject DeathParticleSystemPrefab;
    public Transform ExplosionPosition;


    public int Health { get { return _health; } private set { _health = value; } }
    public bool IsDestroyed { get { return _isDestroyed; } private set { _isDestroyed = value; } }


    public void TakeDamage(int amountOfDamage)
    {
        _health -= amountOfDamage;

        if (_health <= 0)
        {
            _isDestroyed = true;

            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            DeathParticleSystemPrefab.transform.localScale = ExplosionPosition.localScale;
            Instantiate(DeathParticleSystemPrefab, ExplosionPosition.position, ExplosionPosition.rotation);
        }

    }

}

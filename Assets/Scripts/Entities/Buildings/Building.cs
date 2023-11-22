using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Building : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health = 1;
    public TextMeshProUGUI HealthDisplay;
    [SerializeField] protected bool _isDestroyed = false;
    private ElementType _element;
    [SerializeField] protected GameObject DeathParticleSystemPrefab;
    [SerializeField] protected Transform ExplosionPosition;


    [SerializeField] protected bool _immuneToFire = false;
    [SerializeField] protected bool _immuneToWater = false;


    public ElementType Element { get { return _element; } private set { _element = value; } }
    public int Health { get { return _health; } private set { _health = value; } }
    public bool IsDestroyed { get { return _isDestroyed; } private set { _isDestroyed = value; } }


    private void Start()
    {
        if (_health > 1)
        {
            if (HealthDisplay != null)
            {
                GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
           

        }
    }

    private void Update()
    {
        if (HealthDisplay != null)
        {
            HealthDisplay.text = _health.ToString();
        }
        
    }


    public virtual void TakeDamage(int amountOfDamage, ElementType elementype)
    {
        if (elementype == ElementType.Water && _immuneToWater == false ||
            elementype == ElementType.Fire && _immuneToFire == false ||
            elementype == ElementType.None)
        {
            _health -= amountOfDamage;
            Debug.Log("Taking damage: " + amountOfDamage);
        }


        if (_health <= 0)
        {
            _isDestroyed = true;

            if (HealthDisplay != null)
            {
                GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }

            DeathParticleSystemPrefab.transform.localScale = ExplosionPosition.localScale;
            Instantiate(DeathParticleSystemPrefab, ExplosionPosition.position, ExplosionPosition.rotation);

            for (int i = 0; i < gameObject.transform.childCount - 1; i++)
            {
                if (gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>() != null)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
                }
                
            }
        }
    }


    public void MakeImmuneTo(ElementType item)
    {
        if (item == ElementType.Water)
        {
            _immuneToWater = true;
        }
        if (item == ElementType.Fire)
        {
            _immuneToFire = true;
        }
    }


}

public enum ElementType
{
    None,
    Water,
    Fire
}
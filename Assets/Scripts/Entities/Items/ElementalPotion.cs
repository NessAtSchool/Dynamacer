using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalPotion : PowerUp
{
    [SerializeField] private ElementType _element = ElementType.None;
    [SerializeField] private Color _powerUpColor;


    //TO MAKE IT SO THAT BOTTLE ONLY DISSEAPPEARS IF YOU LET GO IF IT ON THE BOMB
    public override void OnTriggerEnter(Collider collision)
    {
        if (gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.GetComponentInParent<Bomb>() != null)
        {
            _isTouching = true;
            _bombThatIsAffected = collision.gameObject.GetComponentInParent<Bomb>();
           
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.GetComponentInParent<Bomb>() != null)
        {
            _isTouching = true;
            _bombThatIsAffected = collision.gameObject.GetComponentInParent<Bomb>();

        }
    }


    public override void ApplyEffect()
    {
        _bombThatIsAffected.ModifyElement(_element);

        _bombThatIsAffected.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = _powerUpColor;

        Destroy(gameObject);

    }
}

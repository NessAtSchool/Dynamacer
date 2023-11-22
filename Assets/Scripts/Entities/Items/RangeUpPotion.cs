using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpPotion : PowerUp 
{
    [SerializeField] private int _rangeUpAmount = 1;

    //TO MAKE IT SO THAT BOTTLE ONLY DISSEAPPEARS IF YOU LET GO IF IT ON THE BOMB
    public override void OnTriggerEnter(Collider collision)
    {

        if (gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.transform.parent != GridManager.Instance._inventory.transform && collision.gameObject.GetComponentInParent<Bomb>() != null)
        {
            Bomb bomb = collision.gameObject.GetComponentInParent<Bomb>();
            bomb.ModifyRange(_rangeUpAmount);

            bomb.transform.GetChild(2).gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }



}

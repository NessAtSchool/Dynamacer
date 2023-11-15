using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpPotion : Draggable
{
    [SerializeField] private int _rangeUpAmount = 1;


    private void OnTriggerEnter(Collider collision)
    {
        //print("hit " + collision.gameObject.name);

        if (gameObject.transform.parent != GridManager.Instance._inventory.transform)
        {
            print("hit bomb");
            collision.gameObject.GetComponentInParent<Bomb>().ModifyRange(_rangeUpAmount);
            Destroy(gameObject);
        }
    }

}

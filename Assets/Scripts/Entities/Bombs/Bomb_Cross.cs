using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Cross : Bomb
{

    public override void HighlightBobShape(Transform parentTile)
    {
        AffectedArea.Clear();

        float x = parentTile.localPosition.x;
        float y = parentTile.localPosition.y;


        print($"{x},{y}");
        foreach (Transform tile in GridManager.Instance.Tiles)
        {
            for (int i = 0; i < range; i++)
            {
                if (tile.gameObject.name == $"Tile {x},{y + i}" || tile.gameObject.name == $"Tile {x + i},{y}" ||
                    tile.gameObject.name == $"Tile {x},{y - i}" || tile.gameObject.name == $"Tile {x - i},{y}")
                {
                    AffectedArea.Add(tile.gameObject.GetComponent<Tile>());
                }
            }


        }

        foreach (Tile tile in AffectedArea)
        {
            tile.gameObject.GetComponent<AffectedAreaOutline>().enabled = true;
        }
    }



}

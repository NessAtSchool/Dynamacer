using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Circle : Bomb
{

    public override void HighlightBobShape(Transform parentTile)
    {
        AffectedArea.Clear();

        int x = (int)parentTile.localPosition.x;
        int y = (int)parentTile.localPosition.y;

        //TODO IMPLEMENT CIRCLE METHOD

        //print($"{x},{y}");

        foreach (Transform tile in GridManager.Instance.Tiles)
        {
            for (int i = 0; i < _range; i++)
            {
                if (tile.gameObject.name == $"Tile {x},{y + i}" || tile.gameObject.name == $"Tile {x + i},{y}" ||
                    tile.gameObject.name == $"Tile {x},{y - i}" || 
                    tile.gameObject.name == $"Tile {x - i},{y}" ||tile.gameObject.name == $"Tile {x + i},{y + i}" ||
                    tile.gameObject.name == $"Tile {x - i},{y - i}" ||
                    tile.gameObject.name == $"Tile {x + i},{y - i}" || tile.gameObject.name == $"Tile {x - i},{y + i}")
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

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

        print($"{x},{y}");
        //for (int i = 0; i < range; i++)
        //{
        //    for (int j = 0; j < range; j++)
        //    {
        //        int tileX = x - i;
        //        int tileY = y - j;

        //        // Calculate distance between the current tile and the center
        //        float distance = Mathf.Sqrt(Mathf.Pow(tileX - centerX, 2) + Mathf.Pow(tileY - centerY, 2));

        //        // Check if the distance is within the circular area
        //        if (distance <= 5)
        //        {
        //            // This tile is within the circular area
        //            string tileName = $"Tile {tileX},{tileY}";
        //            if (IsTileValid(tileName))
        //            {
        //                // Assuming IsTileValid is a function to check if the tile exists or fits certain conditions
        //                Tile tile = GetTileFromName(tileName);
        //                AffectedArea.Add(tile);
        //            }
        //        }
        //    }
        //}
    }
}

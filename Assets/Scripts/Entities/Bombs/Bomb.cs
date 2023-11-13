using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Draggable
{
    public List<Tile> AffectedArea;
    public int range = 3;

    public virtual void HighlightBobShape( Transform parentTile)
    {
        return;
    }

    

}

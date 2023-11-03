using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : BasicTile
{
    [SerializeField] private Material _baseOffsetMaterial;
   

    public override void Init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        _renderer.material = isOffset ? _baseOffsetMaterial : _baseMaterial;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{

    [SerializeField] private Material _grassMaterial;

    public override void Init(int x, int y)
    {
        _renderer.material = _grassMaterial;
        transform.rotation =Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z);
        
        //var isOffset = (x + y) % 2 == 1;
        //GetComponent<MeshFilter>().mesh = isOffset ? _baseOffsetMesh : _baseMesh;
    }
    

}

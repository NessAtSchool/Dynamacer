using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //TODO CREATE A SAVE SYSTEM TOO ALLOWS YOU TO SAVE AND MODIFY GRIDS INCLUDING THE BOMBS AND TOWNS ON THEM

    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] BasicTile _grassTile, _waterTile;
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _grid;

    private float transformWidth;
    private float transformHeight;
 
    //[SerializeField] private Dictionary<Vector3, BasicTile> _tiles;

    private void Awake()
    {
        Instance = this;
        _cam = Camera.main;

        
    }

    public void GenerateGrid()
    {

        if (_grid != null)
        {
            Destroy(GameObject.Find("Grid"));
            _grid = new GameObject("Grid");
        }
        else
        {
            _grid = new GameObject("Grid");
        }

        transformHeight = 1f / _height;
        transformWidth = 1f / _width;

        float scaleX = _grassTile.transform.localScale.x;
        float scaleY = _grassTile.transform.localScale.z;

        if (transformHeight > transformWidth)
        {
            transformHeight = transformWidth;
        }
        else if (transformWidth > transformHeight)
        {
            transformWidth = transformHeight;
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //spwan water tiles randomly in grassland
                //var randomTile = Random.Range(0, 6) == 2 ? _waterTile : _grassTile;
                //var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.Euler(90, 0, 0));
               

                //All Grass
                var spawnedTile = Instantiate(_grassTile, new Vector3((x*transformWidth * scaleX), (y*transformHeight * scaleY)), Quaternion.Euler(90, 0, 0));
               
                spawnedTile.transform.parent = _grid.transform;
                spawnedTile.transform.localScale = new Vector3(scaleX * transformWidth, spawnedTile.transform.localScale.y, scaleY * transformHeight);
                spawnedTile.name = $"Tile {x},{y}";         
                spawnedTile.Init(x,y);

                //tiles[new Vector3(x, y)] = spawnedTile;
            }
        }


        //_cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -zDistance);

        GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);
    }


}

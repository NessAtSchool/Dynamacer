using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] BasicTile _grassTile, _waterTile;
    [SerializeField] private Transform _cam;
    [SerializeField] private GameObject _grid;

    private Dictionary<Vector2, BasicTile> _tiles;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.SetUp)
        {
            GenerateGrid();
        }
        
    }

   

    private void Start()
    {
       
    }


    void GenerateGrid()
    {
        _grid = new GameObject("Grid");

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //spwan water tiles randomly in grassland
                var randomTile = Random.Range(0, 6) == 2 ? _waterTile : _grassTile;

                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.Euler(90, 0, 0));
                spawnedTile.transform.parent = _grid.transform;

                //All Grass
                //var spawnedTile = Instantiate(_grassTile, new Vector3(x, y), Quaternion.Euler(90, 0, 0));

                spawnedTile.name = $"Tile {x},{y}";
                               
                spawnedTile.Init(x,y);

                //_tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -20);
    }

    public BasicTile GetTileAtPosition(Vector3 pos) 
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}

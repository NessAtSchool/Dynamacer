using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] Tile _grassTile, _waterTile;
    [SerializeField] private Camera _cam;
    [SerializeField] public GameObject _grid;
    public GameObject _inventory;
    public GameObject OrginalGrid;
    public List<Transform> Tiles;
    private List<Bomb> _bombsToDetenate = new List<Bomb>();
    public List<GameObject> StartingBombs = new List<GameObject>();
    public GameObject CopyKeeper;

    private void Awake()
    {
        Instance = this;
        _cam = Camera.main;

        PrepareReset();
    }

    
    public void GenerateGrid()
    {
        Tiles.Clear();

        if (_grid.transform.childCount > 0)
        {
                foreach (Transform tile in _grid.transform)
                {
                   Destroy(tile.gameObject);
                }
        }
         

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //spwan water tiles randomly in grassland
                //var randomTile = Random.Range(0, 6) == 2 ? _waterTile : _grassTile;

                //var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.Euler(90, 0, 0));
                

                //All Grass
                var spawnedTile = Instantiate(_grassTile, new Vector3(x , y , _grid.transform.position.z), Quaternion.Euler(90, 0, 0));

                Tiles.Add(spawnedTile.transform);
                spawnedTile.transform.SetParent(_grid.transform);
                spawnedTile.name = $"Tile {x},{y}";

                spawnedTile.Init(x, y);
                
            }
        }

        //float zDistance;

        //if (_width > _height)
        //{
        //    zDistance = -_width;
        //}
        //else if (_height > _width)
        //{
        //    zDistance = -_height;
        //}
        //else
        //{
        //    zDistance = -_width;
        //}

        //_cam.transform.position = new Vector3((float)_grid.transform.position.x / 2 - 0.5f, (float)_grid.transform.position.y / 2 - 0.5f, zDistance);
        GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);
    }

    IEnumerator DetenateAllTheBombs()
    {
        foreach (Bomb bomb in _bombsToDetenate)
        {
            if (bomb != null)
            {
                bomb.Detenate();
            }
        }

        GameManager.Instance.UpdateGameState(GameManager.GameState.Resolve);
        yield return new WaitForSeconds(1);
    }


    public void StartDetenation()
    {
        _bombsToDetenate.Clear();
        foreach (Transform tile in Tiles)
        {
            if (tile.GetComponentInChildren<Bomb>() != null)
            {
                _bombsToDetenate.Add(tile.GetComponentInChildren<Bomb>());
            }
        }

        StartCoroutine(DetenateAllTheBombs());

    }

    public void PrepareReset() 
    {
        OrginalGrid = Instantiate(_grid);
        OrginalGrid.SetActive(false);

        if (StartingBombs.Count <= 0)
        {
            foreach (Transform bomb in _inventory.transform)
            {
                GameObject bombReset = Instantiate(bomb.gameObject, CopyKeeper.transform);
                StartingBombs.Add(bombReset);
                bombReset.SetActive(false);
            }

        }
    

        Tiles.Clear();
        foreach (Transform tile in _grid.transform)
        {
            Tiles.Add(tile.transform);
        }

    }

}
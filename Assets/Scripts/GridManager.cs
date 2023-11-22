using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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
    [SerializeField] private int _turns = 1;
    [SerializeField] private int _turnsLeft;
    public TextMeshProUGUI TurnTracker;


    public int TurnsLeft { get { return _turnsLeft; } private set { _turnsLeft = value; } }

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
    
                //All Grass
                var spawnedTile = Instantiate(_grassTile, new Vector3(x , y , _grid.transform.position.z), Quaternion.Euler(90, 0, 0));

                Tiles.Add(spawnedTile.transform);
                spawnedTile.transform.SetParent(_grid.transform);
                spawnedTile.name = $"Tile {x},{y}";

                spawnedTile.Init(x, y);
                
            }
        }

         GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);
    }

    IEnumerator DetonateAllTheBombs()
    {
        foreach (Bomb bomb in _bombsToDetenate)
        {
            if (bomb != null)
            {
                foreach (Tile tile in bomb.AffectedArea)
                {
                    StartCoroutine(tile.HandleBombDetonation(bomb.Element));
                }

            }
        }

        foreach (Bomb bomb in _bombsToDetenate)
        {
            bomb.Detonate();
        }

        GameManager.Instance.UpdateGameState(GameManager.GameState.Resolve);
        yield return new WaitForSeconds(1);
    }


    public void StartDetonation()
    {
        UpdateTurn(-1);
        
        _bombsToDetenate.Clear();
        foreach (Transform tile in Tiles)
        {
            if (tile.GetComponentInChildren<Bomb>() != null)
            {
                _bombsToDetenate.Add(tile.GetComponentInChildren<Bomb>());
            }
        }

        StartCoroutine(DetonateAllTheBombs());

    }

 
    public void PrepareReset() 
    {
        OrginalGrid = Instantiate(_grid);
        OrginalGrid.SetActive(false);
        ResetTurn();

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

    private void UpdateTurn(int increment)
    {
        _turnsLeft += increment;

        TurnTracker.text = _turnsLeft.ToString();
    }

    private void ResetTurn()
    {
        _turnsLeft = _turns;

        TurnTracker.text = _turnsLeft.ToString();
    }

}
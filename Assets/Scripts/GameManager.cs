using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Add a menu manager later to control the popups when you win/lose Check tarodev video again
    
    public static GameManager Instance;
    private GridManager _gridManager;
    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gridManager = GridManager.Instance;

        if (_gridManager._grid == null)
        {
            UpdateGameState(GameState.GenerateGrid);
        }
        else
        {
            UpdateGameState(GameState.SetUp);
        }
       
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                break;
            case GameState.SetUp:
                HandleSetUp();
                break;
            case GameState.Execution:
                HandleExecution();
                break;
            case GameState.Resolve:
                HandleResolve();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLoss();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        
    }

    private bool IsDyomancyComplete()
    {
        foreach (Transform tile in _gridManager.Tiles)
        {
            if (tile.GetComponentInChildren<IDamageable>() != null)
            {
                if (tile.GetComponentInChildren<IDamageable>().IsDestroyed == false)
                {
                    return false;
                } 
                
            }
        }

        return true;
    }

    private void HandleLoss()
    {
        print("BOO! They're still alive!");
    }

    private void HandleVictory()
    {
        print("YAY! Dynamancy Complete!");
    }
    private async void HandleResolve()
    {
        
        await Task.Delay(1000);

        if (IsDyomancyComplete() == true)
        {
            UpdateGameState(GameState.Victory);
        }
        else
        {
            UpdateGameState(GameState.Lose);
        }

    }
    private void HandleExecution()
    {
        _gridManager.StartDetenation();
    }
    public void HandleExecutionButton()
    {
        HandleExecution();
    }

    private void HandleSetUp()
    {
        
    }

    //DO THIS WITH SCIPTABLE OBJECTS IN THE FEATURE!!!!
    public void Reset()
    {
        var oldGrid = GameObject.Find("Grid");
        Destroy(oldGrid);
        _gridManager.OrginalGrid.SetActive(true);
        _gridManager._grid = _gridManager.OrginalGrid;
        _gridManager._grid.name = "Grid";


        foreach (GameObject bomb in _gridManager._inventory.transform)
        {
            Destroy(bomb);
        }

        foreach (GameObject bomb in _gridManager.StartingBombs)
        {
            bomb.SetActive(true);
            bomb.transform.SetParent(_gridManager._inventory.transform);
            
        }

        


        _gridManager.PrepareReset();


        GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);

    }


    public enum GameState { 
    
        GenerateGrid,
        SetUp,
        Execution,
        Resolve,
        Victory,
        Lose
    
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Add a menu manager later to control the popups when you win/lose Check tarodev video again


    public static GameManager Instance;
    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
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
        //Add way to check if everything has been destoryed and if not return false
        return true;
    }

    private void HandleLoss()
    {
        
    }

    private void HandleVictory()
    {
        
    }
    private async void HandleResolve()
    {
        
        await Task.Delay(500);

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
        
    }

    private void HandleSetUp()
    {
        
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    //Add a menu manager later to control the popups when you win/lose Check tarodev video again
    
    public static GameManager Instance;
    private GridManager _gridManager;
    public GameState State;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject lossPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject panelLocation;


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

        canvas = GameObject.FindWithTag("Canvas");
        victoryPanel = GameObject.Find("VictoryPanel");
        lossPanel = GameObject.Find("LossPanel");
        panelLocation = GameObject.Find("PanelLocation");
        

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

    private bool GridCleared()
    {
        foreach (Transform tile in _gridManager.Tiles)
        {
   
            if (tile.GetComponentInChildren<IDamageable>() != null)
            {

                if (tile.GetComponentInChildren<IDamageable>().IsDestroyed == false && 
                    tile.GetComponentInChildren<IDamageable>() is not Tower_Building)
                {
                    if (tile.GetComponentInChildren<IDamageable>() is Blockade == true)
                    {
                        break;
                    }
                    else
                    {
                        return false;
                    }
                   
                } 
                else if (tile.GetComponentInChildren<IDamageable>().IsDestroyed == true && 
                    tile.GetComponentInChildren<IDamageable>() is Tower_Building)
                {

                    return false;
                }
       
            }

           
        }

        return true;
     
    }

    [Preserve]
    private void HandleLoss()
    {
        lossPanel.transform.position = panelLocation.transform.position;
        
    }

    [Preserve]
    private void HandleVictory()
    {
        victoryPanel.transform.position = panelLocation.transform.position;

    }
    private async void HandleResolve()
    {
        
        await Task.Delay(1000);

        if (GridCleared() == true)
        {
            UpdateGameState(GameState.Victory);
        }
        else
        {
            if (GridManager.Instance.TurnsLeft <= 0)
            {
                UpdateGameState(GameState.Lose);
            }
            else
            {
                UpdateGameState(GameState.SetUp);
            }
            
        }

    }
    private void HandleExecution()
    {
        _gridManager.StartDetonation();
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

        foreach (Transform bomb in _gridManager._inventory.transform)
        {
            Destroy(bomb.gameObject);

        }

        foreach (Transform bomb in _gridManager.CopyKeeper.transform)
        {
            Instantiate(bomb, _gridManager._inventory.transform);
        }

        foreach(Transform bomb in _gridManager._inventory.transform)
        {
            bomb.gameObject.SetActive(true);
        }

        _gridManager.PrepareReset();


        GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);

    }

    public void Next()
    {
        int currentLvlInt = int.Parse(RemoveLettersExample.RemoveLetters(SceneManager.GetActiveScene().name));
            currentLvlInt++;
         
            if (currentLvlInt <= 12)
            {
                SceneManager.LoadScene($"Level_{currentLvlInt}");
            }
            else
            {
                SceneManager.LoadScene("End");
                print("Thank you for playing you've reached the end of this demo!");
            }
         
    }

    public void Previous()
    {
        int currentLvlInt = int.Parse(RemoveLettersExample.RemoveLetters(SceneManager.GetActiveScene().name));
        currentLvlInt--;

        if (currentLvlInt > 1)
        {
            SceneManager.LoadScene($"Level_{currentLvlInt}");
        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }

    public void RedoBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

public class RemoveLettersExample
{
    public static void Main()
    {
        string inputString = "abc123def456gh";
        string result = RemoveLetters(inputString);

        Console.WriteLine("Result: " + result);
    }

    public static string RemoveLetters(string input)
    {
        // Use regular expression to remove letters
        string result = Regex.Replace(input, "[^0-9]", "");

        return result;
    }
}
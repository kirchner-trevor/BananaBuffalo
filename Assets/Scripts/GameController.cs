using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameStates State = GameStates.StartOfGame;
    public int Turn = 1;
    public int LastTurnNumber = 28;
    public int Level = -1;
    public int Score = 0;

    public UnityEvent GameStartStarted;
    public UnityEvent SwapStarted;
    public UnityEvent GrowStarted;
    public UnityEvent HarvestStarted;
    public UnityEvent SelectionStarted;
    public UnityEvent GameEndStarted;

    public UnityEvent<string> StateChanged;

    public enum GameStates
    {
        StartOfGame = 0,
        Swap = 1,
        Grow = 2,
        Harvest = 3,
        Selection = 4,
        EndOfGame = 5
    }

    public void NextState()
    {
        switch(State)
        {
            case GameStates.StartOfGame:
                State = GameStates.Swap;
                SwapStarted?.Invoke();
                StateChanged?.Invoke(State.ToString());
                break;
            case GameStates.Swap:
                State = GameStates.Grow;
                GrowStarted?.Invoke();
                StateChanged?.Invoke(State.ToString());
                break;
            case GameStates.Grow:
                State = GameStates.Harvest;
                HarvestStarted?.Invoke();
                StateChanged?.Invoke(State.ToString());
                break;
            case GameStates.Harvest:
                if (Turn < LastTurnNumber)
                {
                    State = GameStates.Selection;
                    SelectionStarted?.Invoke();
                    StateChanged?.Invoke(State.ToString());
                }
                else
                {
                    State = GameStates.EndOfGame;
                    PersistState();
                    GameEndStarted?.Invoke();
                    StateChanged?.Invoke(State.ToString());
                }
                break;
            case GameStates.Selection:
                Turn++;
                State = GameStates.Swap;
                SwapStarted?.Invoke();
                StateChanged?.Invoke(State.ToString());
                break;
            default:
                Debug.LogError($"Invalid state transition. State = {State}");
                break;
        }
    }

    public void ChangeScore(int change)
    {
        Score += change;
        Debug.Log("Score: " + Score);
    }

    public void ChangeScore(PlantScriptableObject plant)
    {
        Debug.Log("Plant: " + plant);
        Score += plant.PointsForFullyGrown;
        Debug.Log("Score: " + Score);
    }

    private void PersistState()
    {
        PersistentData.Instance.SetLevel(Level);
        PersistentData.Instance.SetScore(Score);
    }

    // Start is called before the first frame update
    void Start()
    {
        State = GameStates.StartOfGame;
        Turn = 1;
        Level = 1;
        Score = 0;
        GameStartStarted?.Invoke();
        StateChanged?.Invoke(State.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

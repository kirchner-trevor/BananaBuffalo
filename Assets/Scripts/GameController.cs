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
    public int Score = 0;
      

    public UnityEvent GameStartStarted;
    public UnityEvent SwapStarted;
    public UnityEvent GrowStarted;
    public UnityEvent HarvestStarted;
    public UnityEvent SelectionStarted;
    public UnityEvent GameEndStarted;
    public UnityEvent Turn5;
    public UnityEvent<int> TurnChanged;
    public UnityEvent<int> ScoreChanged;

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
        switch (State)
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
                TurnChanged?.Invoke(Turn);
                State = GameStates.Swap;
                SwapStarted?.Invoke();
                StateChanged?.Invoke(State.ToString());
                break;
            default:
                Debug.LogError($"Invalid state transition. State = {State}");
                break;
        }
        if (Turn == 24)
        {
            Turn5.Invoke();
        }
    }
    public void LoadLevel()
    {
        if (PersistentData.Instance.LevelObject != null)
        {
            Random.InitState(PersistentData.Instance.LevelObject.Seed);
        }
        else
        {
            Debug.LogWarning("GameController - Failed To Load Level - Loading 'Default' Level");
            PersistentData.Instance.SetLevel(1);
            Random.InitState(111111);
        }
    }

    public void ChangeScore(int change)
    {
        Score += change;
        PersistentData.Instance.SetLocalScore(Score);
        Debug.Log("Score: " + Score);
    }

    public void ChangeScore(PlantScriptableObject plant)
    {
        Debug.Log("Plant: " + plant);
        Score += plant.PointsForFullyGrown;
        ScoreChanged?.Invoke(plant.PointsForFullyGrown);
        PersistentData.Instance.SetLocalScore(Score);
        Debug.Log("Score: " + Score);
    }

    private void PersistState()
    {
        PersistentData.Instance.SetLevelScore(Score);
    }

    // Start is called before the first frame update
    void Start()
    {
        State = GameStates.StartOfGame;
        Turn = 1;
        Score = 0;
        PersistentData.Instance.SetLocalScore(Score);
        GameStartStarted?.Invoke();
        StateChanged?.Invoke(State.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

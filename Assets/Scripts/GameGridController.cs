using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameGridController : MonoBehaviour
{
    public List<GameGridRow> Rows;

    public GameGridSpace SwapStartSpace;
    public UnityEvent<GameGridSpace> SwapStarted;
    public UnityEvent<GameGridSpace> SwapTested;
    public UnityEvent<GameGridSpace> SwapCompleted;
    public UnityEvent<GameGridSpace> SwapCanceled;
    public UnityEvent<GameGridSpace> SpaceHighlighted;

    public enum GridState
    {
        None = 0,
        Swapping = 1,
    }

    public GridState State = GridState.None;

    public void StartSwap(GameGridSpace space)
    {
        State = GridState.Swapping;
        SwapStartSpace = space;
        SwapStarted?.Invoke(space);
    }

    public void TestSwap(GameGridSpace space)
    {
        // TODO
        SwapTested?.Invoke(space);
    }

    public void CompleteSwap(GameGridSpace space)
    {
        if (SwapStartSpace != null)
        {
            if (!IsAdjacent(SwapStartSpace, space))
            {
                CancelSwap();
                return;
            }

            State = GridState.None;

            // TODO: Change plant image instead of swapping colors
            PlantData startPlantData = SwapStartSpace.PlantData;
            SwapStartSpace.SetPlantData(space.PlantData);
            space.SetPlantData(startPlantData);

            SwapCompleted?.Invoke(SwapStartSpace);
            SwapCompleted?.Invoke(space);

            SwapStartSpace = null;
        }
        else
        {
            Debug.LogError("Cannot end swap without starting one.");
        }
    }

    public void CancelSwap()
    {
        State = GridState.None;
        SwapCanceled?.Invoke(SwapStartSpace);
        SwapStartSpace = null;
    }

    public void HighlightSpace(GameGridSpace space)
    {
        // TODO
        SpaceHighlighted?.Invoke(space);
    }

    private bool IsAdjacent(GameGridSpace space1, GameGridSpace space2)
    {
        return space1 != null && space2 != null && Mathf.Abs(space1.Row - space2.Row) + Mathf.Abs(space1.Column - space2.Column) <= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        int rowIndex = 0;
        foreach (GameGridRow row in Rows)
        {
            int columnIndex = 0;
            foreach (GameGridSpace space in row.Columns)
            {
                GameGridSpace localSpace = space;
                localSpace.Button.onClick.AddListener(() => SwapSpace(localSpace));
                localSpace.Row = rowIndex;
                localSpace.Column = columnIndex;
                columnIndex++;
            }
            rowIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapSpace(GameGridSpace space)
    {
        switch (State)
        {
            case GridState.None:
                StartSwap(space);
                HighlightSpace(space);
                break;
            case GridState.Swapping:
                if (space != SwapStartSpace)
                {
                    CompleteSwap(space);
                }
                else
                {
                    CancelSwap();
                }
                break;
        }
    }
}

[System.Serializable]
public class GameGridRow
{
    public string Row;
    public List<GameGridSpace> Columns;
}
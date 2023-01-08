using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameGridController : MonoBehaviour
{
    public List<GameGridRow> Rows;
    public List<PlantScriptableObject> AllPlants;
    
    public GameGridSpace SwapStartSpace;
    public UnityEvent<GameGridSpace> SwapStarted;
    public UnityEvent<GameGridSpace> SwapTested;
    public UnityEvent<GameGridSpace> SwapCompleted;
    public UnityEvent<GameGridSpace> SwapCanceled;
    public UnityEvent<GameGridSpace> SpaceHighlighted;

    public UnityEvent<GameGridSpace> SpaceFullyGrown;
    public UnityEvent<GameGridSpace> SpaceFullyDiseased;
    public UnityEvent<PlantScriptableObject> PlantFullyGrown;

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
            Debug.Log("Swap Completed");
            SwapCompleted?.Invoke(SwapStartSpace);
            

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

    public void AddGrowthAndDiseaseToAllSpaces()
    {
        for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
        {
            GameGridRow row = Rows[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Columns.Count; columnIndex++)
            {
                GameGridSpace space = row.Columns[columnIndex];

                if (space == null || space.PlantData.Plant == null)
                {
                    continue;
                }

                // Base Growth and Disease
                int growthAmount = 1;
                int diseaseAmount = -1;

                if (rowIndex > 0)
                {
                    // Top Neighbor
                    growthAmount += space.PlantData.Plant.GetGrowthFrom(Rows[rowIndex - 1].Columns[columnIndex].PlantData.Plant);
                    diseaseAmount += space.PlantData.Plant.GetDiseaseFrom(Rows[rowIndex - 1].Columns[columnIndex].PlantData.Plant);
                }

                if (rowIndex < Rows.Count - 1)
                {
                    // Top Bottom Neighbor
                    growthAmount += space.PlantData.Plant.GetGrowthFrom(Rows[rowIndex + 1].Columns[columnIndex].PlantData.Plant);
                    diseaseAmount += space.PlantData.Plant.GetDiseaseFrom(Rows[rowIndex + 1].Columns[columnIndex].PlantData.Plant);
                }

                if (columnIndex > 0)
                {
                    // Left Neighbor
                    growthAmount += space.PlantData.Plant.GetGrowthFrom(Rows[rowIndex].Columns[columnIndex - 1].PlantData.Plant);
                    diseaseAmount += space.PlantData.Plant.GetDiseaseFrom(Rows[rowIndex].Columns[columnIndex - 1].PlantData.Plant);
                }

                if (columnIndex < Rows.Count - 1)
                {
                    // Right Neighbor
                    growthAmount += space.PlantData.Plant.GetGrowthFrom(Rows[rowIndex].Columns[columnIndex + 1].PlantData.Plant);
                    diseaseAmount += space.PlantData.Plant.GetDiseaseFrom(Rows[rowIndex].Columns[columnIndex + 1].PlantData.Plant);
                }

                // Cannot have negative growth
                space.PlantData.Growth += Mathf.Clamp(growthAmount, 0, growthAmount);
                space.PlantData.Disease += diseaseAmount;

                space.SetPlantData(space.PlantData);

                if (space.PlantData.IsFullyGrown())
                {
                    PlantFullyGrown?.Invoke(space.PlantData.Plant);
                    SpaceFullyGrown?.Invoke(space);
                }

                if (space.PlantData.IsFullyDiseased())
                {
                    SpaceFullyDiseased?.Invoke(space);
                }
            }
        }
    }

    public void AddPlantToEmptySpace(PlantScriptableObject plant)
    {
        AddPlantToEmptySpaces(plant, 1);
    }

    public void AddPlantToEmptySpaces(PlantScriptableObject plant, int count)
    {
        List<GameGridSpace> emptySpaces = new List<GameGridSpace>();
        for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
        {
            GameGridRow row = Rows[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Columns.Count; columnIndex++)
            {
                GameGridSpace space = row.Columns[columnIndex];

                if (space == null || space.PlantData.Plant == null)
                {
                    emptySpaces.Add(space);
                }
            }
        }

        int remaining = count;

        while (remaining-- > 0)
        {
            GameGridSpace emptySpace = emptySpaces.OrderBy(_ => Random.value).FirstOrDefault();

            if (emptySpace != null)
            {
                emptySpace.SetPlantData(new PlantData
                {
                    Plant = plant
                });
            }
            else
            {
                break;
            }
        }
    }

    public void AddPlantsToGridRandomly()
    {
        int gridSpaces = Rows.Sum(_ => _.Columns.Count);
        int spacesPerPlant = gridSpaces / AllPlants.Count;
        foreach (PlantScriptableObject plant in AllPlants)
        {
            Debug.Log($"Adding {spacesPerPlant} {plant.Name} to grid.");
            AddPlantToEmptySpaces(plant, spacesPerPlant);
        }
    }

    public void RemoveWeedsNextToSpace(GameGridSpace space)
    {
        if (space.Row > 0)
        {
            // Top
            RemoveWeedFromSpace(Rows[space.Row - 1].Columns[space.Column]);
        }

        if (space.Row < Rows.Count - 1)
        {
            // Bottom
            RemoveWeedFromSpace(Rows[space.Row + 1].Columns[space.Column]);
        }

        if (space.Column > 0 )
        {
            // Left
            RemoveWeedFromSpace(Rows[space.Row].Columns[space.Column - 1]);
        }

        if (space.Column < Rows[space.Row].Columns.Count - 1)
        {
            //Right
            RemoveWeedFromSpace(Rows[space.Row].Columns[space.Column + 1]);
        }
    }

    private void RemoveWeedFromSpace(GameGridSpace space)
    {
        if (space.PlantData.Plant != null && space.PlantData.Plant.Name == "Weed")
        {
            Debug.Log($"Removed Weed from {space.Row}x{space.Column}.");
            space.Clear();
        }
    }

    public void RemovePlants(PlantScriptableObject plant, int count)
    {
        ChangePlants(space => space != null && space.PlantData.Plant == plant, space => space.Clear(), count);
    }

    public void ChangePlants(System.Func<GameGridSpace, bool> condition, System.Action<GameGridSpace> action, int count)
    {
        List<GameGridSpace> plantSpaces = new List<GameGridSpace>();
        for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
        {
            GameGridRow row = Rows[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Columns.Count; columnIndex++)
            {
                GameGridSpace space = row.Columns[columnIndex];

                if (condition(space))
                {
                    plantSpaces.Add(space);
                }
            }
        }

        int remaining = count;

        while (remaining-- > 0)
        {
            GameGridSpace plantSpace = plantSpaces.OrderBy(_ => Random.value).FirstOrDefault();

            if (plantSpace != null)
            {
                action(plantSpace);
            }
            else
            {
                break;
            }
        }
    }
}

[System.Serializable]
public class GameGridRow
{
    public string Row;
    public List<GameGridSpace> Columns;
}
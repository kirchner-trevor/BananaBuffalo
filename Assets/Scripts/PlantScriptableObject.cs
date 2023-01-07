using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Plant")]
public class PlantScriptableObject : ScriptableObject
{
    public string Name;
    public List<PlantGrowthStage> GrowthStages;
    public int MaxGrowthNeeded;
    public int MaxDiseaseTolerated;
    public List<PlantRelationship> Relationships;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGrowthFrom(PlantScriptableObject plant)
    {
        return GetValueFrom(plant, p => p.GrowthChange);
    }

    public int GetDiseaseFrom(PlantScriptableObject plant)
    {
        return GetValueFrom(plant, p => p.DiseaseChange);
    }

    public int GetValueFrom(PlantScriptableObject plant, Func<PlantRelationship, int> getValue)
    {
        if (plant == null)
        {
            return 0;
        }

        foreach (PlantRelationship relationship in Relationships)
        {
            if (plant == relationship.Plant)
            {
                return getValue(relationship);
            }
        }

        return 0;
    }
}

[System.Serializable]
public class PlantGrowthStage
{
    public int MinGrowthNeeded;
    public Sprite Sprite;
}

[System.Serializable]
public class PlantRelationship
{
    public int GrowthChange;
    public int DiseaseChange;
    public PlantScriptableObject Plant;
}

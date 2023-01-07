using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Plant")]
public class PlantScriptableObject : ScriptableObject
{
    public string Name;
    public List<PlantGrowthStage> GrowthStages;
    public int MaxGrowthNeeded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlantGrowthStage
{
    public int MinGrowthNeeded;
    public Sprite Sprite;
}

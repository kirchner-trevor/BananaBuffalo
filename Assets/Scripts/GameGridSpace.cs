using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGridSpace : MonoBehaviour
{
    public Button Button;
    public Image PlantImage;
    public Image DiseaseImage;

    public int Row;
    public int Column;

    public PlantData PlantData;

    public void Clear()
    {
        SetPlantData(new PlantData());
    }

    public void SetPlantData(PlantData plant)
    {
        PlantData = plant;

        if (PlantData != null && PlantData.Plant != null)
        {
            foreach (PlantGrowthStage stage in PlantData.Plant.GrowthStages)
            {
                if (PlantData.Growth >= stage.MinGrowthNeeded)
                {
                    PlantImage.sprite = stage.Sprite;
                }
            }

            // TODO: Have multiple stages of disease instead of a boolean
            DiseaseImage.enabled = PlantData.Disease > 4;
        }
        else
        {
            // Nothing here, just make it empty looking
            PlantImage.sprite = null;
            DiseaseImage.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlantData(PlantData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlantData
{
    public PlantScriptableObject Plant;
    public int Growth;
    public int Disease;

    public bool IsFullyGrown()
    {
        return Plant != null && Growth >= Plant.MaxGrowthNeeded;
    }

    public bool IsFullyDiseased()
    {
        return Plant != null && Disease >= Plant.MaxDiseaseTolerated;
    }
}
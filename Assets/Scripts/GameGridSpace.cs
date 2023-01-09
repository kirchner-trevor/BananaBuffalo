using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGridSpace : MonoBehaviour
{
    public Button Button;
    public Image PlantImage;
    public Image[] DiseaseImages;

    public int Row;
    public int Column;

    public PlantData PlantData;
    public PlantData LastPlantData;

    public PlantData GetMostRecentPlantData()
    {
        return PlantData.Plant != null ? PlantData : LastPlantData;
    }

    public void Clear()
    {
        if (PlantData.Plant != null)
        {
            LastPlantData = PlantData;
        }
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
                    PlantImage.color = new Color(1, 1, 1, 1f);
                }
            }

            for (int i = 0; i < DiseaseImages.Length; i++)
            {
                DiseaseImages[i].gameObject.SetActive(false);
            }

            float diseasePerImage = PlantData.Plant.MaxDiseaseTolerated / (DiseaseImages.Length + 1);
            for (int i = DiseaseImages.Length - 1; i >= 0; i--)
            {
                float totalDiseaseMin = Mathf.RoundToInt(diseasePerImage * (i + 1));
                if (PlantData.Disease >= totalDiseaseMin)
                {
                    DiseaseImages[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            // Nothing here, just make it empty looking
            PlantImage.sprite = null;
            PlantImage.color = new Color(1, 1, 1, 0.1f);
            for (int i = 0; i < DiseaseImages.Length; i++)
            {
                DiseaseImages[i].gameObject.SetActive(false);
            }
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
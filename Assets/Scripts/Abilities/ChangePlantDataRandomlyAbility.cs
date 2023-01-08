using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangePlantDataRandomlyAbility : MonoBehaviour
{
    public int GrowthChange;
    public int DiseaseChange;
    public int Count;

    public bool DiseasedOnly;

    // Start is called before the first frame update
    void Start()
    {
        GameGridController gameGridController = FindObjectOfType<GameGridController>();

        if (gameGridController != null)
        {
            gameGridController.ChangePlants(space =>
            {
                return space != null && space.PlantData.Plant != null && space.PlantData.Plant.Name != "Weed" &&
                (DiseasedOnly ? space.PlantData.Disease > 0 : true);
            }, space =>
            {
                space.PlantData.Growth += Mathf.Clamp(GrowthChange, 0, GrowthChange);
                space.PlantData.Disease += DiseaseChange;
                space.SetPlantData(space.PlantData);
                Debug.Log($"Changed plant {space.PlantData.Plant.Name} at {space.Row}x{space.Column}: {GrowthChange}G {DiseaseChange}D");
            }, Count);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

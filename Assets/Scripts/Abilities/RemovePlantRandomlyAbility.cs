using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlantRandomlyAbility : MonoBehaviour
{
    public PlantScriptableObject Plant;
    public int Count;

    // Start is called before the first frame update
    void Start()
    {
        GameGridController gameGridController = FindObjectOfType<GameGridController>();

        if (gameGridController != null)
        {
            gameGridController.RemovePlants(Plant, Count);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

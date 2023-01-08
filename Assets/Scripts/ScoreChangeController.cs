using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangeController : MonoBehaviour
{
    public Transform space;
    public GameObject scorePrefab;
    private GameObject scoreObject;
    public void ShowScore() 
    {
        scoreObject = Instantiate(scorePrefab);
        PlaceScore();
    }
    public void PlaceScore()
    {
        scoreObject.transform.SetParent(space, false);
        Debug.Log("Place Score");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

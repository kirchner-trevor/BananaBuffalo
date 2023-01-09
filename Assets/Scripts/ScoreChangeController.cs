using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreChangeController : MonoBehaviour
{
    public Transform space;
    public GameObject scorePrefab;
    private GameObject scoreObject;
    
    public void ShowScore(int score) 
    {
        StartCoroutine(ShowUp(score));
    }

    public void ShowScore(GameGridSpace gameGridSpace)
    {
        StartCoroutine(ShowUp(gameGridSpace));
    }

    IEnumerator ShowUp(int score)
    {
        float randTime = Random.Range(0f, 1f);
        yield return new WaitForSeconds(randTime);
        scoreObject = Instantiate(scorePrefab);
        scoreObject.GetComponentInChildren<TMPro.TMP_Text>().text = score.ToString();

        PlaceScore();
    }

    IEnumerator ShowUp(GameGridSpace gameGridSpace)
    {
        float randTime = Random.Range(0f, 0.5f);
        yield return new WaitForSeconds(randTime);
        scoreObject = Instantiate(scorePrefab);
        scoreObject.GetComponentInChildren<TMPro.TMP_Text>().text = gameGridSpace.GetMostRecentPlantData().Plant.PointsForFullyGrown.ToString();
        scoreObject.transform.SetParent(gameGridSpace.gameObject.transform, false);
    }

    public void PlaceScore()
    {
        float randSpot1 = Random.Range(400,900);
        float randSpot2 = Random.Range(100,600);
        Debug.Log(randSpot2 + "," + randSpot1);

        scoreObject.transform.SetParent(space, false);
        scoreObject.transform.position = new Vector3(randSpot1, randSpot2);
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

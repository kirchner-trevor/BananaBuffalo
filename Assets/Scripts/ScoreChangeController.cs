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
    IEnumerator ShowUp(int score)
    {
        float randTime = Random.Range(0f, 1f);
        yield return new WaitForSeconds(randTime);
        scoreObject = Instantiate(scorePrefab);
        scoreObject.GetComponentInChildren<TMPro.TMP_Text>().text = score.ToString();

        PlaceScore();
    }
    public void PlaceScore()
    {
        float randSpot1 = Random.Range(0,500);
        float randSpot2 = Random.Range(0, 1080);
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

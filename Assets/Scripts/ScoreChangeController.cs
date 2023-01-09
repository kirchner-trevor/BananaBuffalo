using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangeController : MonoBehaviour
{
    public RectTransform space;
    public GameObject scorePrefab;
    private GameObject scoreObject;

    public GameObject heartPrefab;
    private List<GameObject> heartObjects = new List<GameObject>();
    
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

        for (int i = 0; i < Mathf.CeilToInt(score / 1f); i++)
        {
            heartObjects.Add(Instantiate(heartPrefab));
        }

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
        foreach (GameObject heartObject in heartObjects)
        {
            float x = Random.Range(space.rect.width / -2f, space.rect.width / 2f);
            float y = Random.Range(space.rect.height / -2f, space.rect.height / 2f);

            heartObject.transform.SetParent(space, false);
            heartObject.transform.localPosition = new Vector3(x, y);
        }
        heartObjects.Clear();
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

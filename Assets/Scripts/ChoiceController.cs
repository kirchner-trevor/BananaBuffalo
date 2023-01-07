using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public GameObject RandomChoice1;
    public GameObject RandomChoice2;
    public GameObject RandomChoice3;
    public GameObject[] Choices;
    public Transform Space1;
    public Transform Space2;
    public Transform Space3;
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;

    public void GetNewChoices() 
    {
    int Random1 = Random.Range(0, Choices.Length);
    int Random2 = Random.Range(0, Choices.Length);
    int Random3 = Random.Range(0, Choices.Length);
     
    RandomChoice1 = Choices[Random1];
    RandomChoice2 = Choices[Random2];
    RandomChoice3 = Choices[Random3];
        PlaceChoices();
    }
    public void PlaceChoices()
    {
        Object1 = Instantiate(RandomChoice1);
            Object1.transform.SetParent(Space1, false);
        Object2 = Instantiate(RandomChoice2);
            Object2.transform.SetParent(Space2, false);
        Object3 = Instantiate(RandomChoice3);
            Object3.transform.SetParent(Space3, false);
    }
    public void ClearChoices()
    {
    Destroy(Object1);
        Destroy(Object2);
        Destroy(Object3);
    
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

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
        Instantiate(RandomChoice1).transform.SetParent(Space1,false);

        Instantiate(RandomChoice2).transform.SetParent(Space2, false); ;

        Instantiate(RandomChoice3).transform.SetParent(Space3, false); ;
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

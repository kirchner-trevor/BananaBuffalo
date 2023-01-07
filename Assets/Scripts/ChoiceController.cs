using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent<GameAction> ChoiceChosen;

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
        Object1 = PlaceChoice(RandomChoice1, Space1);
        Object2 = PlaceChoice(RandomChoice2, Space2);
        Object3 = PlaceChoice(RandomChoice3, Space3);
    }

    private GameObject PlaceChoice(GameObject prefab, Transform space)
    {
        GameObject object1 = Instantiate(prefab);
        object1.transform.SetParent(space, false);
        GameAction object1GameAction = object1.GetComponentInChildren<GameAction>();
        object1GameAction.UseButton.onClick.AddListener(() => ChoiceChosen?.Invoke(object1GameAction));
        return object1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeafController : MonoBehaviour
{

    
    public Transform Space1;
    public Transform Space2;
    public Transform Space3;
    public GameObject Leaf;


    public GameObject PlaceLeaf(GameObject prefab, Transform space)
    {
        GameObject object1 = Instantiate(prefab);
        object1.transform.SetParent(space, false);
        GameAction object1GameAction = object1.GetComponentInChildren<GameAction>();
        //object1GameAction.UseButton.onClick.AddListener(() => ChoiceChosen?.Invoke(object1GameAction));
        return object1;
    }

    public void ClearChoices()
    {
        Destroy(Leaf);
        
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

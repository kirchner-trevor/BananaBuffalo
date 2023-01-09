using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScoreUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        float randSeconds = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(randSeconds);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

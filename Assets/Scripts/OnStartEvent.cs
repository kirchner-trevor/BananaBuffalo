using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    // Start is called before the first frame update
    void Start()
    {
        OnStart.Invoke();
        StartCoroutine(End());
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(5f);
        OnEnd.Invoke();
        
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}

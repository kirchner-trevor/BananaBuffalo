using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MoonPhaseController : MonoBehaviour
{
    public int turn = 0;
    public UnityEvent Phase1;
    public UnityEvent Phase2;
    public UnityEvent Phase3;
    public UnityEvent Phase4;
    public UnityEvent Phase5;
    public UnityEvent Phase6;
    public UnityEvent Phase7;
    public UnityEvent Phase8;



    public void NextState()
    {
        
        if (turn == 0)
        {
            Phase1.Invoke();
        }
        else if (turn == 3)
        {
            Phase2.Invoke();
        }
        else if(turn == 6)
        {
            Phase3.Invoke();
        }
        else if (turn == 10)
        {
            Phase4.Invoke();
        }
        else if (turn == 14)
        {
            Phase5.Invoke();
        }
        else if (turn == 18)
        {
            Phase6.Invoke();
        }
        else if (turn == 22)
        {
            Phase7.Invoke();
        }
        else if (turn == 25)
        {
            Phase8.Invoke();
        }
        turn++;

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

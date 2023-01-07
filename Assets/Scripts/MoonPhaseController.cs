using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MoonPhaseController : MonoBehaviour
{
    public int Phase = 1;
    public int turn = 1;
    public UnityEvent Phase1;
    public UnityEvent Phase2;
    public UnityEvent Phase3;
    public UnityEvent Phase4;
    public UnityEvent Phase5;
    public UnityEvent Phase6;
    public UnityEvent Phase7;
    public UnityEvent Phase8;

    public UnityEvent<string> StateChanged;

    public enum GameStates
    {
        Phase1 = 0,
        Phase2 = 1,
        Phase3 = 2,
        Phase4 = 3,
        Phase5 = 4,
        Phase6 = 5,
        Phase7 = 6,
        Phase8 = 7

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

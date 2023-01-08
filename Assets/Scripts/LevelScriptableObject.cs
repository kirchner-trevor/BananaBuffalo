using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public int Level;
    public string Name;
    public int MinBronzeScore;
    public int MinSilverScore;
    public int MinGoldScore;
    public int Seed;
    public Sprite Preview;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
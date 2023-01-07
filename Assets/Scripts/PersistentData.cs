using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int Score { get; private set; }

    public int Level { get; private set; } = -1;

    public Dictionary<int, int> LevelScore = new Dictionary<int, int>();

    public void SetLevel(int level)
    {
        Level = level;

        if (!LevelScore.ContainsKey(Level))
        {
            LevelScore[Level] = 0;
        }
    }

    public void SetScore(int score)
    {
        Score = score;

        if (Level != -1)
        {
            LevelScore[Level] = Score;
        }
    }
}

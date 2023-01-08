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

    public LevelScriptableObject LevelObject;

    public void SetLevel(LevelScriptableObject level)
    {
        Instance._SetLevel(level);
    }

    private void _SetLevel(LevelScriptableObject level)
    {
        LevelObject = level;
        SetLevel(level.Level);
        Debug.Log($"PersistentData - Set Level - {level.Name}");
    }

    public void SetLevel(int level)
    {
        Instance._SetLevel(level);
    }

    private void _SetLevel(int level)
    {
        Level = level;

        if (!LevelScore.ContainsKey(Level))
        {
            LevelScore[Level] = 0;
            Debug.Log($"Persisted Score 0 for Level {Level}.");
        }
    }

    public void SetScore(int score)
    {
        Instance._SetScore(score);
    }

    private void _SetScore(int score)
    {
        Score = score;

        if (Level != -1)
        {
            LevelScore[Level] = Score;
            Debug.Log($"Persisted Score {Score} for Level {Level}.");
        }
    }
}

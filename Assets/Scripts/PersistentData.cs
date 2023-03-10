using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            LoadFromDisk();
            FixHighScores();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int Score { get; private set; }

    public int Level { get; private set; } = -1;

    public Dictionary<int, int> LevelScore = new Dictionary<int, int>();
    public Dictionary<int, int> LevelHighScore = new Dictionary<int, int>();

    public LevelScriptableObject LevelObject;
    public DateTimeOffset SaveTime = DateTimeOffset.UtcNow;

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

    public void SetLevelScore(int score)
    {
        Instance._SetLevelScore(score);
    }

    private void _SetLevelScore(int score)
    {
        Score = score;

        if (Level != -1)
        {
            if (Score > LevelScore[Level])
            {
                LevelHighScore[Level] = Score;
                Debug.Log($"Updated HighScore {Score} for Level {Level}.");
            }
            LevelScore[Level] = Score;
            SaveToDisk();
            Debug.Log($"Persisted Score {Score} for Level {Level}.");
        }
    }

    public void SetLocalScore(int score)
    {
        Instance._SetLocalScore(score);
    }

    private void _SetLocalScore(int score)
    {
        Score = score;
    }

    private void FixHighScores()
    {
        foreach (KeyValuePair<int, int> levelScore in Instance.LevelScore)
        {
            if (Instance.LevelHighScore.TryGetValue(levelScore.Key, out int levelHighScore))
            {
                if (levelScore.Value > levelHighScore)
                {
                    Instance.LevelHighScore[levelScore.Key] = levelScore.Value;
                }
            }
            else
            {
                Instance.LevelHighScore[levelScore.Key] = levelScore.Value;
            }
        }
    }

    private void SaveToDisk()
    {
        Instance.SaveTime = DateTimeOffset.UtcNow;
        string data = JsonUtility.ToJson(new PersistedSaveData
        {
            SaveTime = Instance.SaveTime.ToUnixTimeSeconds(),
            LevelScore = Instance.LevelScore.Select(_ => new LevelScoreData { Level = _.Key, Score = _.Value }).ToArray(),
            LevelHighScore = Instance.LevelHighScore.Select(_ => new LevelScoreData { Level = _.Key, Score = _.Value }).ToArray()
        });
        Debug.Log($"PersistentData - Saving Data To Disk - {data}");
        if (!Directory.Exists(PersistentDataPath))
        {
            Directory.CreateDirectory(PersistentDataPath);
        }
        File.WriteAllText(Path.Combine(PersistentDataPath, "save.json"), data);
    }

    private void LoadFromDisk()
    {
        if (File.Exists(Path.Combine(PersistentDataPath, "save.json")))
        {
            string data = File.ReadAllText(Path.Combine(PersistentDataPath, "save.json"));
            Debug.Log($"PersistentData - Loading Data From Disk - {data}");
            PersistedSaveData saveData = JsonUtility.FromJson<PersistedSaveData>(data);
            if (saveData.LevelScore != null)
            {
                Instance.LevelScore = saveData.LevelScore.ToDictionary(_ => _.Level, _ => _.Score);
            }
            if (saveData.LevelHighScore != null)
            {
                Instance.LevelHighScore = saveData.LevelHighScore.ToDictionary(_ => _.Level, _ => _.Score);
            }
            if (saveData.SaveTime != 0)
            {
                Instance.SaveTime = DateTimeOffset.FromUnixTimeSeconds(saveData.SaveTime);
            }
        }
        else
        {
            Debug.Log("No Save State Exists");
        }
    }

    private string PersistentDataPath
    {
        get
        {
            return Application.platform == RuntimePlatform.WebGLPlayer ? "/idbfs/23356ba0-909e-11ed-a1eb-0242ac120002" : Application.persistentDataPath;
        }
    }
}


[System.Serializable]
public class PersistedSaveData
{
    public long SaveTime;
    public LevelScoreData[] LevelScore;
    public LevelScoreData[] LevelHighScore;
}

[System.Serializable]
public class LevelScoreData
{
    public int Level;
    public int Score;
}
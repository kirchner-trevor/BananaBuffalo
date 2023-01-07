using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelectController : MonoBehaviour
{
    public List<LevelMap> AllLevels;

    public UnityEvent<Transform> LockUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        foreach (LevelMap level in AllLevels)
        {
            if (level.Level.Level == 1)
            {
                LockUnlocked?.Invoke(level.LockInScene);
            }
            else
            {
                int previousLevelNumber = level.Level.Level - 1;
                LevelMap previousLevel = AllLevels.Find(_ => _.Level.Level == previousLevelNumber);
                int previousLevelScore = PersistentData.Instance.LevelScore.GetValueOrDefault(previousLevelNumber);
                Debug.Log($"Checking level {level.Level.Level} for unlock, player has {previousLevelScore} points in the previous level.");
                if (previousLevelScore >= previousLevel.Level.MinBronzeScore)
                {
                    LockUnlocked?.Invoke(level.LockInScene);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class LevelMap
{
    public LevelScriptableObject Level;
    public Transform LockInScene;
}
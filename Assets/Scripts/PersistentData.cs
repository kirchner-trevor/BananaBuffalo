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

    private int score;
    public int Score { get { return Instance.score; } private set { Instance.score = value; } }

    public void SetScore(int score)
    {
        Score = score;
    }
}

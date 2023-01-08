using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoController : MonoBehaviour
{
    public TMP_Text LevelTitleText;
    public TMP_Text BronzeScoreText;
    public TMP_Text SilverScoreText;
    public TMP_Text GoldScoreText;
    public Image LevelPreviewImage;

    // Start is called before the first frame update
    void Start()
    {
        if (PersistentData.Instance.LevelObject)
        {
            LoadLevel(PersistentData.Instance.LevelObject);
        }
        else
        {
            Debug.LogError("LevelInfoController - Failed to Load Level - No Level Persisted");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(LevelScriptableObject level)
    {
        LevelTitleText.text = level.Name;
        BronzeScoreText.text = level.MinBronzeScore.ToString();
        SilverScoreText.text = level.MinSilverScore.ToString();
        GoldScoreText.text = level.MinGoldScore.ToString();
        LevelPreviewImage.sprite = level.Preview;
        Debug.Log($"LevelInfoController - Loaded Level - {level.Name}");
    }
}

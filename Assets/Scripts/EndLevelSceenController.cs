using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelSceenController : MonoBehaviour
{
    public Image MedalImage;

    public Sprite BronzeMedalSprite;
    public Sprite SilverMedalSprite;
    public Sprite GoldMedalSprite;

    // Start is called before the first frame update
    void Start()
    {
        LevelScriptableObject level = PersistentData.Instance.LevelObject;
        if (level != null)
        {
            int levelScore = PersistentData.Instance.LevelScore[PersistentData.Instance.Level];
            if (levelScore >= level.MinGoldScore)
            {
                MedalImage.sprite = GoldMedalSprite;
            }
            else if (levelScore >= level.MinSilverScore)
            {
                MedalImage.sprite = SilverMedalSprite;
            }
            else if (levelScore >= level.MinBronzeScore)
            {
                MedalImage.sprite = BronzeMedalSprite;
            }
            else
            {
                MedalImage.enabled = false;
            }
        }
        else
        {
            Debug.Log($"EndLevelScreenController - No Level Loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

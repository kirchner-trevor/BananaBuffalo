using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMedalForLevel : MonoBehaviour
{
    public LevelScriptableObject Level;

    public Image MedalImage;

    public Sprite BronzeMedal;
    public Sprite SilverMedal;
    public Sprite GoldMedal;

    // Start is called before the first frame update
    void Start()
    {
        if (Level != null && PersistentData.Instance.LevelHighScore.TryGetValue(Level.Level, out int score))
        {
            MedalImage.gameObject.SetActive(true);
            if (score >= Level.MinGoldScore)
            {
                MedalImage.sprite = GoldMedal;
            }
            else if (score >= Level.MinSilverScore)
            {
                MedalImage.sprite = SilverMedal;
            }
            else if (score >= Level.MinBronzeScore)
            {
                MedalImage.sprite = BronzeMedal;
            }
            else
            {
                MedalImage.gameObject.SetActive(false);
            }
        }
        else
        {
            MedalImage.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

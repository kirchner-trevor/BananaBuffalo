using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class TextDataProvider : MonoBehaviour
{
    private TMPro.TMP_Text Text;
    private string originalText;
    private string lastText;

    private void Awake()
    {
        Text = GetComponent<TMPro.TMP_Text>();
    }

    private void FixedUpdate()
    {
        // If we haven't captured the text, or it has changed since we last modified it, reset.
        if (originalText == null || lastText != Text.text)
        {
            originalText = Text.text;
        }

        lastText = originalText;
        lastText = lastText.Replace("<score>", PersistentData.Instance.Score.ToString());
        lastText = lastText.Replace("<level-score>", PersistentData.Instance.LevelScore[PersistentData.Instance.Level].ToString());
        lastText = lastText.Replace("<level-name>", PersistentData.Instance.LevelObject?.Name);
        lastText = lastText.Replace("<value>", Value);

        Text.text = lastText;
    }

    public string Value;

    public void SetValue(int value)
    {
        Value = value.ToString();
    }
}

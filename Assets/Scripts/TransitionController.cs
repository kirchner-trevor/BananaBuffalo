using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLevelInfo()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToEndLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(5);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

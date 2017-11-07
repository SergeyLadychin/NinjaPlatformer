using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public bool godModeEnabled;
    public float delayAfterPlayerDeath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //DontDestroyOnLoad(instance);
	}

    void OnEnable()
    {
        EventManager.StartListen(Constants.GoToMainMenuEvent, GoToMainMenu);
    }

    void OnDisable()
    {
        EventManager.StopListen(Constants.GoToMainMenuEvent, GoToMainMenu);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void ReloadCurrentLevel()
    {
        PickUpManager.Restore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopPlaying()
    {
        Time.timeScale = 0.0f;
    }

    public void StartPlaying()
    {
        Time.timeScale = 1.0f;
    }

    public void StartNewGame()
    {
        StartPlaying();
        LoadLevel("Level1");
    }

    public void GoToNextLevel(string nextLevelName)
    {
        PickUpManager.Save();
        LoadLevel(nextLevelName);
    }

    public void GoToMainMenu()
    {
        PickUpManager.ResetCounts();
        LoadLevel("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

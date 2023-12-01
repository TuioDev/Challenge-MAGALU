using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const string SCENE_MENU = "Menu";
    public const string SCENE_LEVEL_SELECTION = "LevelSelection";
    public const string SCENE_CONTROLS = "Controls";
    public const string SCENE_SETTINGS = "Settings";
    public const string SCENE_LEVEL = "Level";
    public const string SCENE_CREDITS = "Credits";

    public void LoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadControls()
    {
        SceneManager.LoadScene(SCENE_CONTROLS);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(SCENE_SETTINGS);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(SCENE_CREDITS);
    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene(SCENE_LEVEL_SELECTION);
    }

    public void LoadLevelIndex(int index)
    {
        if (index < 0) return;

        SceneManager.LoadScene(SCENE_LEVEL + index.ToString());
    }
    
    public void LoadNextLevel()
    {
        int sceneCurrent = SceneManager.GetActiveScene().buildIndex;

        int sceneNext = (sceneCurrent < SceneManager.sceneCountInBuildSettings - 1) 
            ? sceneCurrent + 1 : 1; // 1 is the LevelSelection index, can't get through name
        SceneManager.LoadScene(sceneNext);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const string SCENE_MENU = "Menu";
    public const string SCENE_GAMEPLAY = "GamePlay";
    public const string SCENE_CONTROLS = "Controls";
    public const string SCENE_SETTINGS = "Settings";
    public const string SCENE_CREDITS = "Credits";

    public void LoadGameplay()
    {
        SceneManager.LoadScene(SCENE_GAMEPLAY);
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const string SCENE_MENU = "Menu";
    public const string SCENE_GAMEPLAY = "Gameplay";

    public void LoadGameplay()
    {
        SceneManager.LoadScene(SCENE_GAMEPLAY);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }
}

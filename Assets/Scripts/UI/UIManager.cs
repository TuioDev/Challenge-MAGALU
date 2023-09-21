using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject VictoryObject;
    [SerializeField] private GameObject LostObject;

    public void SetGameVictory()
    {
        VictoryObject.SetActive(true);
        Time.timeScale = 0f; // Try to put the Time Manager in an event when the game is over
    }

    public void SetGameLost()
    {
        LostObject.SetActive(true);
        Time.timeScale = 0f; // Try to put the Time Manager in an event when the game is over
    }

    public void RestartLevel()
    {
        // TODO: CHANGE THIS SO IT DOESN'T HAS TO GET STRING NAME
        SceneManager.LoadScene("GamePlay");
    }
}

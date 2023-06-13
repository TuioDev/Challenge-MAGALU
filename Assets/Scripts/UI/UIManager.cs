using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject VictoryObject;
    [SerializeField] private GameObject LostObject;

    public void Awake()
    {
        Time.timeScale = 1f; // THIS SHOULD BE A JOB FOR THE TIME MANAGER, HE SUBSCRIBES AS A LISTENER AND CHANGES TIME!
    }
    public void SetGameVictory()
    {
        VictoryObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetGameLost()
    {
        LostObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // TODO: CHANGE THIS SO IT DOESN'T HAS TO GET STRING NAME
        SceneManager.LoadScene("GamePlay");
    }
}

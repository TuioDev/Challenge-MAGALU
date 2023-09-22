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
        // Play audio?
    }

    public void SetGameLost()
    {
        LostObject.SetActive(true);
        // Play audio?
    }
}

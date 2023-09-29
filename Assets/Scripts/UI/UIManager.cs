using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float WaitForTimer = 4.0f;
    [Header("UI References")]
    [SerializeField] private GameObject VictoryObject;
    [SerializeField] private GameObject LostObject;
    [Header("Event References")]
    [SerializeField] private GameEvent OnPanelVictory;
    [SerializeField] private GameEvent OnPanelLost;

    public void SetGameVictory()
    {
        StartCoroutine(WaitAndActivatePanel(VictoryObject, OnPanelVictory));
    }

    public void SetGameLost()
    {
        StartCoroutine(WaitAndActivatePanel(LostObject, OnPanelLost));
    }

    private IEnumerator WaitAndActivatePanel(GameObject panel, GameEvent gameEvent)
    {
        yield return new WaitForSecondsRealtime(WaitForTimer);
        panel.SetActive(true);
        gameEvent.TriggerEvent();
    }
}

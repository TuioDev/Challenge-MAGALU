using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using NaughtyAttributes;
using System;
using System.Threading;

public enum LevelType
{
    Tutorial,
    Gameplay
}

public class LevelManager : MonoBehaviour
{
    [Header("General information")]
    [SerializeField] private GameObject LevelSpawner;
    [SerializeField] private GameObject LevelTimer;
    [SerializeField] private LevelType LevelInfo;
    [SerializeField] private DialogHandler LevelDialogHandler;
    [ShowIf(nameof(LevelInfo), LevelType.Tutorial)]
    [SerializeField] private DialogBox TutorialDialog;
    [Header("Tutorial info")]
    [SerializeField] private float InitialWaitTime;

    void Start()
    {
        StartCoroutine(WaitAndOpenDialog());
    }

    private IEnumerator WaitAndOpenDialog()
    {
        yield return new WaitForSeconds(InitialWaitTime);

        LevelDialogHandler.StartDialog(TutorialDialog, this);
    }

    public void StartLevel()
    {
        LevelSpawner.SetActive(true);
        LevelTimer.SetActive(true);
    }
}

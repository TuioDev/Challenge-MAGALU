using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using System.Threading;
using Unity.VisualScripting;

public enum LevelType
{
    Tutorial,
    Gameplay
}

public class LevelManager : MonoBehaviour
{
    [Header("General information")]
    [SerializeField] private ManagersReference Managers;
    [SerializeField] private LevelType LevelInfo;
    [SerializeField] private DialogHandler LevelDialogHandler;
    [ShowIf(nameof(LevelInfo), LevelType.Tutorial)]
    [SerializeField] private DialogBox TutorialDialog;
    [Header("Tutorial info")]
    [SerializeField] private float InitialWaitTime;

    private GameObject EnemySpawnerObject;
    private GameObject TimeManagerObject;

    void Awake()
    {
        TimeManagerObject = Managers.GetTimeManager();
        EnemySpawnerObject = Managers.GetEnemySpawner();
    }

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
        EnemySpawnerObject.SetActive(true);
        TimeManagerObject.SetActive(true);
    }
}

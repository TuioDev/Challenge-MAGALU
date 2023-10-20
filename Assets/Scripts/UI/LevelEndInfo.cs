using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndInfo : MonoBehaviour
{
    [Header("Pie visual info")]
    [SerializeField] private SpriteRenderer PieSpriteReference;
    [Header("Level End stats")]
    [SerializeField] private FloatVariable CurrentLevelPoints;

    private Image PieStateInfo;

    void Start()
    {
        SetPieVisual();
        SetLevelPoints();
    }

    private void SetPieVisual()
    {
        PieStateInfo = GetComponent<Image>();
        PieStateInfo.sprite = PieSpriteReference.sprite;
    }

    private void SetLevelPoints()
    {
        string currentLevel = StatsManager.POINTS_LEVEL + SceneManager.GetActiveScene().name[5..];

        PlayerPrefs.SetInt(currentLevel, (int) CurrentLevelPoints.Value);
        PlayerPrefs.Save();
    }
}

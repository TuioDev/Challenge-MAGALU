using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelsReference;
    [SerializeField] private Sprite NoPointSprite;
    [SerializeField] private Sprite GotPointSprite;
    [Header("Reference to the IsMobile SO, so it doesn't reset")]
    [SerializeField] private BoolVariable IsMobile; // This SO is reseting when not being referenced before a level

    private LevelPoints[] LevelsPointsReference;

    public const string CURRENT_LEVEL = "CurrentLevel";
    public const string POINTS_LEVEL = "PointsLevel";

    void Start()
    {
        SetLevelPoints();
        UpdatePointsUI(GotPointSprite);
        // Show the levels that the player can play
        // The other will be locked with a sprite
        // Check the video that shows that
    }

    private void SetLevelPoints()
    {
        LevelsPointsReference = LevelsReference.GetComponentsInChildren<LevelPoints>();
    }

    private void UpdatePointsUI(Sprite sprite)
    {
        for (int i = 0; i < LevelsPointsReference.Length; i++)
        {
            LevelsPointsReference[i].ChangePointsSprites(sprite, PlayerPrefs.GetInt(POINTS_LEVEL + (i + 1)));
        }
    }

    public void ResetLevelsPoints()
    {
        if (LevelsPointsReference == null) return;

        UpdatePointsUI(NoPointSprite);

        for (int i = 0; i < LevelsPointsReference.Length; i++)
        {
            PlayerPrefs.SetInt(POINTS_LEVEL + (i + 1), 0);
        }
    }

    //public void MaxLevel(int points)
    //{
    //    if (LevelsPointsReference == null) return;

    //    for (int i = 0; i < LevelsPointsReference.Length; i++)
    //    {
    //        PlayerPrefs.SetInt(POINTS_LEVEL + (i + 1), points);
    //    }

    //    UpdatePointsUI(GotPointSprite);
    //}
}

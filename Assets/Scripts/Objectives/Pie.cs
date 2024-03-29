using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private SpriteRenderer PieSprite;
    [SerializeField] private Sprite[] SpriteArray;
    [Header("Events")]
    [SerializeField] private GameEvent OnGameOver;
    [Header("Variables")]
    [SerializeField] private FloatVariable CurrentLevelPoints;

    private int SpriteIndex = 0;

    void Awake()
    {
        CurrentLevelPoints.Value = 5;
    }

    public void ChangeToNextSprite()
    {
        CurrentLevelPoints.Value--;
        SpriteIndex++;

        PieSprite.sprite = SpriteArray[SpriteIndex];

        if (SpriteIndex >= SpriteArray.Length - 1)
        {
            OnGameOver.TriggerEvent();
        }
    }
}

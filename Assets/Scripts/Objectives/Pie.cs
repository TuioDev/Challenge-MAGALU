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

    private int SpriteIndex = 0;

    public void ChangeToNextSprite()
    {
        SpriteIndex++;
        PieSprite.sprite = SpriteArray[SpriteIndex];
        if (SpriteIndex >= SpriteArray.Length - 1)
        {
            OnGameOver.TriggerEvent();
        }
    }

    // TODO: IF THE GAME STARTS AGAIN, ALL STATS RESETS
}

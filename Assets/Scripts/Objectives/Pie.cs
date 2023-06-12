using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PieSprite;
    [SerializeField] private Sprite[] SpriteArray;

    private int SpriteIndex = 0;

    public void ChangeToNextSprite()
    {
        SpriteIndex++;
        if (SpriteIndex >= SpriteArray.Length) return;
        PieSprite.sprite = SpriteArray[SpriteIndex];
    }

    // TODO: IF THE GAME STARTS AGAIN, ALL STATS RESETS
}

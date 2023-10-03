using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndInfo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PieSpriteReference;

    private Image PieStateInfo;

    void Start()
    {
        PieStateInfo = GetComponent<Image>();
        PieStateInfo.sprite = PieSpriteReference.sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] private Sprite CharacterSprite;
    [SerializeField] private string SpeakerName;
    [TextArea(3, 5)]
    [SerializeField] private string TextLine;

    public Sprite Character => CharacterSprite;
    public string Line => TextLine;
    public string Name => SpeakerName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] private DialogCharacterState AnimatorState;
    [SerializeField] private string SpeakerName;
    [TextArea(3, 5)]
    [SerializeField] private string TextLine;

    public DialogCharacterState State => AnimatorState;
    public string Line => TextLine;
    public string Name => SpeakerName;
}

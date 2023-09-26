using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "AudioCollection", menuName = "Audio Collection")]
public class AudioCollection : ScriptableObject
{
    [Header("Audio collection info")]
    [RangedFloat(0, 2, RangedFloatAttribute.RangeDisplayType.EditableRanges)]
    [SerializeField] private RangedFloat Pitch;

    [SerializeField] private AudioClip[] Clips;

    public float GetRandomPitch() => Random.Range(Pitch.min, Pitch.max);
    public AudioClip GetRandomAudioClip() => 
        Clips.Length > 1 ? Clips[Random.Range(0, Clips.Length)] : Clips[0];
}

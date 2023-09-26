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

    [SerializeField] private List<AudioClip> ClipList;

    public List<AudioClip> GetClipList() => ClipList;
    public float GetRandomPitch() => Random.Range(Pitch.min, Pitch.max);
    public AudioClip GetRandomAudioClip() => 
        ClipList.Count > 1 ? ClipList[Random.Range(0, ClipList.Count)] : ClipList[0];
}

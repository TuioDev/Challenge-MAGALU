using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogBox", menuName = "Dialog Box")]
public class DialogBox : ScriptableObject
{
    [Header("Dialog Info")]
    [SerializeField] private GameObject DialogPrefab;
    [SerializeField] private float TextSpeed;
    [Header("Dialog lines")]
    [SerializeField] private Dialog[] Conversation;

    public Dialog[] GetConversation => Conversation;
    public GameObject Prefab => DialogPrefab;
    public float Speed => TextSpeed;
}

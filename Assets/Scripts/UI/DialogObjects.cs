using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogObjects : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private TextMeshProUGUI TextTMP;
    [SerializeField] private Animator DialogCharacterAnimator;
    [SerializeField] private Button MobileButton;

    public TextMeshProUGUI Name => NameTMP;
    public TextMeshProUGUI Text => TextTMP;
    public Animator CharacterAnimator => DialogCharacterAnimator;
    public Button InteractButton => MobileButton;
}

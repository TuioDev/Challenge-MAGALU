using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogObjects : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private TextMeshProUGUI TextTMP;
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Button MobileButton;

    public TextMeshProUGUI Name => NameTMP;
    public TextMeshProUGUI Text => TextTMP;
    public Image Character => CharacterImage;
    public Button InteractButton => MobileButton;
}

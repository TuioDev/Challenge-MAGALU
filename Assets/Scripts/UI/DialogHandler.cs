using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogHandler : MonoBehaviour
{
    [SerializeField] private GameObject CanvasReference;
    [SerializeField] private InputHandler PlayerInputHandler;
    [SerializeField] private BoolVariable IsMobile;

    private DialogBox CurrentDialog;
    private LevelManager CurrentLevelManager;

    private GameObject DialogPrefab;
    private DialogObjects DialogReferences;
    private Animator DialogCharacterAnimator;

    private bool IsTyping;
    private int CurrentLine;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        IsTyping = false;
    }

    public void StartDialog(DialogBox dialogBox, LevelManager levelManager)
    {
        CurrentLine = 0;
        CurrentDialog = dialogBox;
        CurrentLevelManager = levelManager;

        DialogPrefab = Instantiate(dialogBox.Prefab);
        DialogPrefab.transform.SetParent(CanvasReference.transform, false);

        // Get references from the instantiated prefab
        DialogReferences = DialogPrefab.GetComponent<DialogObjects>();
        DialogCharacterAnimator = DialogReferences.CharacterAnimator;

        // Change player action map or if is mobile setactive the button
        if (!IsMobile.Value)
        {
            PlayerInputHandler.ChangeToInteractActionMap();
        }
        else
        {
            DialogReferences.InteractButton.gameObject.SetActive(true);
            DialogReferences.InteractButton.onClick.AddListener(delegate { InteractWithDialog(); });
        }

        StartTyping();
    }

    private void StartTyping()
    {
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        IsTyping = true;

        // Clear the content in the dialog box
        DialogReferences.Name.text = "";
        DialogReferences.Text.text = "";

        // Change animator state
        DialogCharacterAnimator.SetTrigger(CurrentDialog.GetConversation[CurrentLine].State.ToString());

        // Change the name of the current line speaker
        DialogReferences.Name.text = CurrentDialog.GetConversation[CurrentLine].Name;

        // Type the conversation text
        foreach (char c in CurrentDialog.GetConversation[CurrentLine].Line.ToCharArray())
        {
            DialogReferences.Text.text += c;
            yield return new WaitForSeconds(CurrentDialog.Speed);
        }
        IsTyping = false;
    }

    public void FinishLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractWithDialog();
        }
    }

    private void InteractWithDialog()
    {
        if (IsTyping)
        {
            StopAllCoroutines();
            DialogReferences.Text.text = "" + CurrentDialog.GetConversation[CurrentLine].Line;
            IsTyping = false;
        }
        else
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        CurrentLine++;
        if (CurrentLine >= CurrentDialog.GetConversation.Length)
        {
            EndDialog();
            return;
        }
        StartTyping();
    }

    private void EndDialog()
    {
        // Destroy the dialog game object
        Destroy(DialogPrefab);

        // Reset the properties
        DialogPrefab = null;
        CurrentDialog = null;

        // Start the game
        CurrentLevelManager.StartLevel();
        PlayerInputHandler.ChangeToGameplayActionMap();
    }
}

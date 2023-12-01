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

    private DialogBox CurrentDialog;

    private GameObject DialogPrefab;
    private DialogObjects DialogReferences;

    private bool IsTyping;
    private int CurrentLine;
    private LevelManager CurrentLevelManager;

    void Start()
    {
        IsTyping = false;
    }

    public void StartDialog(DialogBox dialogBox, LevelManager levelManager)
    {
        // Change player action map
        PlayerInputHandler.ChangeToInteractActionMap();

        CurrentLine = 0;
        CurrentDialog = dialogBox;


        DialogPrefab = Instantiate(dialogBox.Prefab);
        DialogPrefab.transform.SetParent(CanvasReference.transform, false);

        DialogReferences = DialogPrefab.GetComponent<DialogObjects>();

        CurrentLevelManager = levelManager;

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

using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueNodeSO startingNode;
    [SerializeField] private DialogueManager dialogueManager;

    private string tempDialogueText;

    // void Start()//test
    // {
    //     TriggerStartDialogue();
    // }

    public void TriggerStartDialogue()
    {
        tempDialogueText = startingNode.dialogueText;
        Debug.Log("temp Dialogue: " + tempDialogueText);

        startingNode.dialogueText = $"{startingNode.dialogueText} heyyo";//test
        Debug.Log("Triggering start dialogue");
        dialogueManager.StartDialogue(startingNode);

        startingNode.dialogueText = tempDialogueText;//reset test
        Debug.Log("starting Dialogue after temp: " + startingNode.dialogueText);
    }

    public void TriggerChangeDialogue(DialogueChoice choice)
    {

        switch (choice)
        {
            case DialogueChoice.AskPrice:
                dialogueManager.ChangeNode((int)DialogueChoice.AskPrice);
                break;
            case DialogueChoice.AcceptPrice:
                dialogueManager.ChangeNode((int)DialogueChoice.AcceptPrice);
                break;
            case DialogueChoice.RefusePrice:
                dialogueManager.ChangeNode((int)DialogueChoice.RefusePrice);
                break;
        }

    }

    public enum DialogueChoice
    {
        AskPrice = 0,
        AcceptPrice = 1,
        RefusePrice = 2
    }
}

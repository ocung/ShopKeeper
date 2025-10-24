using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueNodeSO startingNode;
    [SerializeField] private DialogueManager dialogueManager;

    private string tempDialogueText;
    
    public enum DialogueChoice
    {
        AskPrice = 0,
        AcceptPrice = 1,
        RefusePrice = 2
    }
    // void Start()//test
    // {
    //     TriggerStartDialogue();
    // }

    public void TriggerStartDialogue(int hagglePrice)
    {
        tempDialogueText = startingNode.dialogueText;
        Debug.Log("temp Dialogue: " + tempDialogueText);

        startingNode.dialogueText = $"{startingNode.dialogueText} {hagglePrice}";//test
        Debug.Log("Triggering start dialogue");
        dialogueManager.StartDialogue(startingNode);

        startingNode.dialogueText = tempDialogueText;//reset test
        Debug.Log("starting Dialogue after temp: " + startingNode.dialogueText);
    }

    public void TriggerStartDialogue()
    {
        dialogueManager.StartDialogue(startingNode);
    }

    public void TriggerChangeDialogue(DialogueChoice choice, int customerOffer)
    {

        switch (choice)
        {
            case DialogueChoice.AskPrice:
                tempDialogueText = startingNode.choices[(int)DialogueChoice.AskPrice].dialogueText;
                Debug.Log("temp Dialogue: " + tempDialogueText);

                startingNode.choices[(int)DialogueChoice.AskPrice].dialogueText = $"{startingNode.choices[(int)DialogueChoice.AcceptPrice].dialogueText} {customerOffer}";//test
                Debug.Log("Triggering change dialogue: Ask Price");
                dialogueManager.ChangeNode((int)DialogueChoice.AskPrice);

                startingNode.choices[(int)DialogueChoice.AskPrice].dialogueText = tempDialogueText;//reset test
                break;
            case DialogueChoice.AcceptPrice:
                dialogueManager.ChangeNode((int)DialogueChoice.AcceptPrice);
                break;
            case DialogueChoice.RefusePrice:
                dialogueManager.ChangeNode((int)DialogueChoice.RefusePrice);
                break;
        }

    }


}

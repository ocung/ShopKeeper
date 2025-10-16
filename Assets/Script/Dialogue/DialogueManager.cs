using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;

    private DialogueNodeSO currentNode;

    void Start()
    {
        HideDialogBox();
    }

    public void StartDialogue(DialogueNodeSO startingNode)
    {
        currentNode = startingNode;
        DisplayNode();
    }

    public void ChangeNode(int choiceNode)
    {
        if (currentNode.choices.Count <= choiceNode)
        {
            Debug.Log("Invalid choice index");
            return;
        }
        currentNode = currentNode.choices[choiceNode];
        DisplayNode();
    }

    private void DisplayNode()
    {
        ShowDialogBox();
        speakerNameText.text = currentNode.speakerName;
        dialogueText.text = currentNode.dialogueText;
    }

    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(2f);
        HideDialogBox();
    }

    private void HideDialogBox()
    {
        gameObject.SetActive(false);
    }

    private void ShowDialogBox()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(WaitAndHide());
    }

}

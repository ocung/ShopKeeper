using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "DialogueNode")]
public class DialogueNodeSO : ScriptableObject
{

    public string speakerName;
    [TextArea(2, 5)] public string dialogueText;

    public List<DialogueNodeSO> choices = new List<DialogueNodeSO>();
    
}

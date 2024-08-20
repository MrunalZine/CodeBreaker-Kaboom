using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTypewriter dialogueTypeWriter;  // Reference to the DialogueTypeWriter script

    private bool hasTriggered = false;  // Prevent the trigger from activating more than once

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasTriggered)
        {
            dialogueTypeWriter.TriggerDialogue();
            hasTriggered = true;  // Ensure this trigger only happens once
        }
    }
}

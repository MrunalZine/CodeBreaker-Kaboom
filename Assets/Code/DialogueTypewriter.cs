using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTypewriter : MonoBehaviour
{
    public TextMeshProUGUI[] dialogueTextBoxes;  // Array to hold multiple TextMeshPro components
    public string[] dialogueTexts;               // Array to hold corresponding dialogue strings
    public float typingSpeed = 0.05f;            // Speed of typing
    private int currentDialogueIndex = 0;        // To track the current dialogue

    private bool isTyping = false;               // Check if typing is in progress

    private void Update()
    {
        if (isTyping) return; // Prevent triggering new dialogues while typing
    }

    public void TriggerDialogue()
    {
        if (currentDialogueIndex < dialogueTextBoxes.Length && !isTyping)
        {
            StartCoroutine(TypeDialogue(dialogueTextBoxes[currentDialogueIndex], dialogueTexts[currentDialogueIndex]));
            currentDialogueIndex++; // Move to the next dialogue index
        }
    }

    private IEnumerator TypeDialogue(TextMeshProUGUI textBox, string text)
    {
        isTyping = true;
        textBox.text = "";  // Clear the text box before starting

        foreach (char letter in text.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Wait for a bit before typing the next character
        }

        isTyping = false;  // Typing is done, allow the next trigger
    }
}

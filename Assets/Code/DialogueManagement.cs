using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManagement : MonoBehaviour
{
    public TextMeshProUGUI initialTextMeshProUGUI; // TextMeshProUGUI for the initial lines
    public TextMeshProUGUI newTextMeshProUGUI;     // TextMeshProUGUI for the new lines
    public string[] initialLines;                  // Array to hold initial lines of dialogue
    public string[] newLines;                      // Array to hold new lines of dialogue
    public float typingSpeed = 0.05f;              // Speed at which text is typed out
    public float blinkSpeed = 0.5f;                // Speed of the blinking cursor

    private int currentLineIndex = 0;
    private int newLineIndex = 0;
    private bool isTyping = false;
    private bool isInitialDialogueComplete = false;
    private bool isAllDialogueComplete = false;

    void Start()
    {
        initialTextMeshProUGUI.text = "";
        newTextMeshProUGUI.text = "";
        if (initialLines.Length > 0)
        {
            StartCoroutine(DisplayText(initialLines[currentLineIndex], initialTextMeshProUGUI, OnInitialTextComplete));
        }
    }

    void Update()
    {
        if (isAllDialogueComplete)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // All dialogue is done, transition to the next scene or perform some other action
                // SceneManager.LoadScene("NextScene"); // Example: load the next scene
            }
        }
        else if (!isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isInitialDialogueComplete)
                {
                    currentLineIndex++;
                    if (currentLineIndex < initialLines.Length)
                    {
                        StartCoroutine(DisplayText(initialLines[currentLineIndex], initialTextMeshProUGUI, OnInitialTextComplete));
                    }
                    else
                    {
                        isInitialDialogueComplete = true;
                        if (newLines.Length > 0)
                        {
                            StartCoroutine(DisplayText(newLines[newLineIndex], newTextMeshProUGUI, OnNewTextComplete));
                        }
                        else
                        {
                            isAllDialogueComplete = true;
                            StartCoroutine(BlinkCursor(newTextMeshProUGUI));
                        }
                    }
                }
                else
                {
                    newLineIndex++;
                    if (newLineIndex < newLines.Length)
                    {
                        StartCoroutine(DisplayText(newLines[newLineIndex], newTextMeshProUGUI, OnNewTextComplete));
                    }
                    else
                    {
                        isAllDialogueComplete = true;
                        StartCoroutine(BlinkCursor(newTextMeshProUGUI));
                    }
                }
            }
        }
    }

    IEnumerator DisplayText(string line, TextMeshProUGUI textMeshProUGUI, System.Action onComplete)
    {
        isTyping = true;
        foreach (char letter in line.ToCharArray())
        {
            textMeshProUGUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        textMeshProUGUI.text += "\n"; // Move to the next line
        isTyping = false;
        onComplete?.Invoke();
    }

    IEnumerator BlinkCursor(TextMeshProUGUI textMeshProUGUI)
    {
        string lastLine = textMeshProUGUI.text.TrimEnd('\n'); // Get the last line without extra newlines
        while (true)
        {
            textMeshProUGUI.text = lastLine + "_";
            yield return new WaitForSeconds(blinkSpeed);
            textMeshProUGUI.text = lastLine;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void OnInitialTextComplete()
    {
        // Optionally do something when the initial text is complete
    }

    void OnNewTextComplete()
    {
        // Optionally do something when the new text is complete
    }
}

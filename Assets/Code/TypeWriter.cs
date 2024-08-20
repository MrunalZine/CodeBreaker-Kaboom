using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriter : MonoBehaviour
{
    [Tooltip("The TextMesh Pro component where the text will appear.")]
    public TextMeshProUGUI textComponent;

    [Tooltip("Time in seconds between each character.")]
    public float timeBetweenCharacters = 0.05f;

    [Tooltip("Time in seconds for the blinking effect.")]
    public float blinkInterval = 0.5f;

    [Tooltip("The lines of text to display, one line per element.")]
    [TextArea(3, 10)]
    public string[] lines;

    [Tooltip("The name of the scene to load after the last line.")]
    public string nextSceneName;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isComplete = false;
    private bool isBlinking = false;

    void Start()
    {
        textComponent.text = "";
        StartCoroutine(DisplayTextLineByLine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            if (isComplete)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (currentLineIndex < lines.Length)
            {
                StartCoroutine(DisplayTextLineByLine());
            }
        }
    }

    IEnumerator DisplayTextLineByLine()
    {
        isTyping = true;

        // Prepare to add the new line
        string existingText = textComponent.text;
        string newText = existingText;

        // If there are previous lines, add a new line character
        if (!string.IsNullOrEmpty(existingText))
        {
            newText += "\n";
        }

        // Add the current line of text
        string currentLineText = lines[currentLineIndex];
        string typedText = "";

        foreach (char letter in currentLineText.ToCharArray())
        {
            typedText += letter;
            textComponent.text = newText + typedText + "_"; // Display current text with blinking underscore
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        // Remove the blinking underscore once the line is fully typed
        textComponent.text = newText + typedText;
        isBlinking = true;
        StartCoroutine(BlinkUnderscore());

        // Wait until Enter is pressed to move to the next line
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        // Stop blinking and move to the next line
        isBlinking = false;
        currentLineIndex++;
        isTyping = false;

        if (currentLineIndex >= lines.Length)
        {
            isComplete = true; // Mark the dialogue as complete
        }
    }

    IEnumerator BlinkUnderscore()
    {
        while (isBlinking)
        {
            if (textComponent.text.EndsWith("_"))
            {
                textComponent.text = textComponent.text.Remove(textComponent.text.Length - 1); // Hide underscore
            }
            else
            {
                textComponent.text += "_"; // Show underscore
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordTrigger : MonoBehaviour
{
    public TextMeshProUGUI passwordTextBox;  // The TextMeshPro component where the password will appear
    public string passwordText;              // The password string to display
    public float typingSpeed = 0.05f;        // Speed of typing

    private bool isTriggered = false;

    private void Update()
    {
        if (isTriggered)
        {
            StartCoroutine(TypePassword());
            isTriggered = false;  // Ensure it only starts typing once
        }
    }

    private IEnumerator TypePassword()
    {
        passwordTextBox.text = "";  // Clear the text box before starting

        foreach (char letter in passwordText.ToCharArray())
        {
            passwordTextBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Wait for a bit before typing the next character
        }

        // Optional: Add a blinking cursor effect at the end of the typing
        StartCoroutine(BlinkingCursor());
    }

    private IEnumerator BlinkingCursor()
    {
        while (true)
        {
            passwordTextBox.text += "_";
            yield return new WaitForSeconds(0.5f);
            passwordTextBox.text = passwordTextBox.text.Substring(0, passwordTextBox.text.Length - 1);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TriggerTypingEffect()
    {
        isTriggered = true;
    }
}

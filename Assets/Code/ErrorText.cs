using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string firstText = "Error";
    public string secondText = "Press Enter to try again.";
    public float typingSpeed = 0.05f;

    private void OnEnable()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textMeshPro.text = "";

        // Type the first text
        foreach (char letter in firstText.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Add a short pause after the first text
        yield return new WaitForSeconds(0.5f);

        // Move to the next line for the second text
        textMeshPro.text += "\n";

        // Type the second text
        foreach (char letter in secondText.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

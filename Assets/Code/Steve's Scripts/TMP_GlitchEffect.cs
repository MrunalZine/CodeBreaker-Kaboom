using UnityEngine;
using TMPro;
using System.Collections;

public class TMP_GlitchEffect : MonoBehaviour
{
    public TMP_Text glitchText;
    public float glitchDuration = 0.1f;
    public float glitchInterval = 0.5f;

    private string originalText;

    void Start()
    {
        if (glitchText == null)
        {
            glitchText = GetComponent<TMP_Text>();
        }
        originalText = glitchText.text;
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(glitchInterval);

            string glitchedText = originalText;
            for (int i = 0; i < glitchedText.Length; i++)
            {
                if (Random.value < 0.2f) // 20% chance to glitch a character
                {
                    char randomChar = (char)Random.Range(33, 126); // ASCII range for visible characters
                    glitchedText = glitchedText.Remove(i, 1).Insert(i, randomChar.ToString());
                }
            }
            glitchText.text = glitchedText;

            yield return new WaitForSeconds(glitchDuration);

            glitchText.text = originalText;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HorizontalButtonNavigation : MonoBehaviour
{
    [Header("Button Controls")]
    public Button[] buttons; // Assign your buttons in the Inspector
    private int currentIndex = 0;

    [Header("Grow & Shrink Breathing")]
    public float breathingSpeed = 0.1f; // Speed of the scale breathing effect
    public float breathingScale = 1.1f; // Scale multiplier for the breathing effect
    private Vector3 originalScale;

    [Header("Audio Controls")]
    public AudioClip switchSound; // Assign your switch sound in the Inspector
    public AudioClip clickSound; // Assign your click sound in the Inspector
    private AudioSource audioSource;

    [Header("Alpha Breathing")]
    public float minAlpha = 0.0f; // Minimum alpha value for the breathing effect (completely invisible)
    public float maxAlpha = 1.0f; // Maximum alpha value for the breathing effect (fully visible)
    public float alphaBreathingSpeed = 0.7f; // Speed of the alpha breathing effect
    private Color originalColor;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Add listeners to all buttons for mouse clicks
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners(); // Clear any existing listeners to avoid duplicates
            button.onClick.AddListener(() => StartCoroutine(OnButtonClick(button)));
        }

        originalScale = buttons[currentIndex].transform.localScale;
        originalColor = buttons[currentIndex].GetComponent<Image>().color;
        SelectButton(currentIndex); // Start by selecting the first button
    }

    void Update()
    {
        HandleNavigation();

        // Apply the breathing effect to the currently selected button
        ApplyBreathingEffect();
    }

    void HandleNavigation()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : buttons.Length - 1;
            PlaySwitchSound();
            SelectButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex = (currentIndex < buttons.Length - 1) ? currentIndex + 1 : 0;
            PlaySwitchSound();
            SelectButton(currentIndex);
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Press Enter to click the button
        {
            StartCoroutine(OnButtonClick(buttons[currentIndex]));
        }
    }

    void ApplyBreathingEffect()
    {
        // Scale effect
        float scale = 1 + Mathf.PingPong(Time.time * breathingSpeed, breathingScale - 1);
        buttons[currentIndex].transform.localScale = originalScale * scale;

        // Alpha (transparency) effect with separate speed
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * alphaBreathingSpeed, 1));
        Color newColor = originalColor;
        newColor.a = alpha;
        buttons[currentIndex].GetComponent<Image>().color = newColor;
    }

    void SelectButton(int index)
    {
        // Reset the scale and alpha of the previously selected button
        if (currentIndex >= 0 && currentIndex < buttons.Length)
        {
            buttons[currentIndex].transform.localScale = originalScale;
            buttons[currentIndex].GetComponent<Image>().color = originalColor;
        }

        // Select the new button
        buttons[index].Select();
        currentIndex = index;
    }

    IEnumerator OnButtonClick(Button button)
    {
        PlayClickSound();

        // Wait until the click sound finishes playing
        yield return new WaitForSeconds(clickSound.length);

        // Execute the button's intended action directly here
        if (button.name == "YourSceneChangeButtonName") // Replace with your button's name
        {
            SceneManager.LoadScene("YourSceneName"); // Replace with your scene name
        }
        else
        {
            // Handle other button actions here
            button.onClick.Invoke(); // This will trigger any other attached listeners
        }
    }

    void PlaySwitchSound()
    {
        if (switchSound != null)
        {
            audioSource.PlayOneShot(switchSound);
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}

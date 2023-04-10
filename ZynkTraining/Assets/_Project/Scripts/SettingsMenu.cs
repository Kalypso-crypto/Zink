using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }
    public InputField healthInputField;
    public Button increaseHealthButton;
    public Button decreaseHealthButton;

    [SerializeField] Slider volumeSlider;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }

    public void Display()
    {
        gameObject.SetActive(true);
        Controller.Instance.DisplayCursor(true);
        UpdateHealthValueText();
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }


    public void SetPlayerHealth()
    {
        if (healthInputField != null && Player.Instance != null)
        {
            if (int.TryParse(healthInputField.text, out int health))
            {
                Player.Instance.SetHealth(health);
                UpdateHealthDisplay();
            }
        }
    }

    public void IncreasePlayerHealth()
    {
        if (Player.Instance != null)
        {
            int health = Player.Instance.GetCurrentHealth() + 10;
            Player.Instance.SetHealth(health);
            UpdateHealthDisplay();
            UpdateHealthValueText();
        }
    }

    public void DecreasePlayerHealth()
    {
        if (Player.Instance != null)
        {
            int health = Player.Instance.GetCurrentHealth() - 10;
            Player.Instance.SetHealth(health);
            UpdateHealthDisplay();
            UpdateHealthValueText();
        }
    }

    private void UpdateHealthDisplay()
    {
        if (Player.Instance != null && healthInputField != null)
        {
            int health = Player.Instance.GetCurrentHealth();
            healthInputField.text = health.ToString();
            UpdateHealthValueText();
        }
    }

    private void UpdateHealthValueText()
    {
        if (Player.Instance != null)
        {
            HealthBar healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetHealth(Player.Instance.GetCurrentHealth());
            }
        }
    }

    public void Close()
    {
        UIAudioPlayer.PlayNegative();
        gameObject.SetActive(false);
        Controller.Instance.DisplayCursor(false);
        PauseMenu.Instance.Display();
    }
}

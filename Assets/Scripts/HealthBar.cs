using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Added for Slider
using TMPro; // Added for TMP_Text

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Corrected from 'gealthSlider'
    public TMP_Text healthBarText;

    private DamageComponent playerDamageable; // Added declaration

    private void Awake()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.Log("No player found"); // Corrected spelling
        }
        playerDamageable = Player.GetComponent<DamageComponent>();
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth); // Corrected from 'CalculatedSliderPercentage'
        healthBarText.text = "HP " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }

    public void OnEnable() // Corrected method name from 'Enabled'
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged); // Corrected method name from 'AddListennner'
    }

    public void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged); // Corrected method name from 'RemoveListenner'
    }

    private float CalculateSliderPercentage(int currentHealth, int maxHealth) // Corrected method name from 'CalculatedSliderPercentage'
    {
        return (float)currentHealth / maxHealth; // Cast to float for correct division
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

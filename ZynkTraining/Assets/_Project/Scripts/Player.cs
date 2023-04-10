// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;

// public class Player : MonoBehaviour
// {

// 	public static Player Instance { get; private set; }

// 	public int maxHealth = 100;
// 	public int currentHealth;

// 	public HealthBar healthBar;
//  	public PostProcessVolume postProcessVolume;

// 	private float timeSinceLastDamage = 0f;
// 	private bool shouldResetVignette = false;

// 	private Vignette vignette;

//     // Start is called before the first frame update
//     void Start()
//     {
// 		Instance = this;
// 		currentHealth = maxHealth;
// 		healthBar.SetMaxHealth(maxHealth);

// 		// Obținem componenta Vignette din PostProcessVolume
//         postProcessVolume.profile.TryGetSettings(out vignette);
//     }

//     void Update()
// 	{
		
// 		timeSinceLastDamage += Time.deltaTime;
// 		if (timeSinceLastDamage >= 3f && shouldResetVignette)
// 		{
// 			ResetVignette();
// 		}
// 	}

// 	public void TakeDamage(int damage)
// 	{
// 		currentHealth -= damage;

// 		healthBar.SetHealth(currentHealth);

// 		 // Calculăm factorul de intensitate al Vignette-ului
//         float intensity = Mathf.Clamp01((float)currentHealth / maxHealth);

//         // Setăm noua intensitate a Vignette-ului
//         vignette.intensity.value = intensity;


// 		timeSinceLastDamage = 0f;
//     	shouldResetVignette = true;
// 	}

// 	void ResetVignette()
// 	{
// 		Debug.Log("S-a resetat! ");
// 		if (postProcessVolume.profile.TryGetSettings(out vignette))
// 		{
// 			vignette.intensity.value = 0f;

// 		}

// 		shouldResetVignette = false;
// 	}

// 	public void SetHealth(int newHealth)
// 	{
// 		currentHealth = newHealth;
// 		healthBar.SetHealth(currentHealth);
// 	}

// 	public int GetCurrentHealth()
//     {
//         return currentHealth;
//     }

// 	void IncreaseHealth(int amount)
// 	{
// 		currentHealth += amount;

// 		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

// 		healthBar.SetHealth(currentHealth);
// 	}

// 	void DecreaseHealth(int amount)
// 	{
// 		currentHealth -= amount;

// 		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

// 		healthBar.SetHealth(currentHealth);
// 	}
// }





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public PostProcessVolume postProcessVolume;

    private Vignette vignette;

    private bool isTakingDamage = false;
    private float timeSinceLastDamage = 0f;

    private const float VIGNETTE_RESET_TIME = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Obținem componenta Vignette din PostProcessVolume
        postProcessVolume.profile.TryGetSettings(out vignette);
		InvokeRepeating("IncreaseHealthOverTime", 1f, 5f);
    }

    void Update()
    {
        // Dacă jucătorul nu mai primește damage de 3 secunde, resetăm vignette-ul
        if (!isTakingDamage)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= 3f)
            {
                vignette.intensity.value = 0f;
                timeSinceLastDamage = 0f;
            }
        }
        else
        {
            timeSinceLastDamage = 0f;
        }

        // Update the vignette intensity based on the player's health
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = Mathf.Lerp(1.0f, 0.0f, (float)currentHealth / maxHealth);
            float intensity = Mathf.Clamp01((float)currentHealth / maxHealth);
            
        }
    }


    public void TakeDamage(int damage)
    {
        isTakingDamage = true;
        timeSinceLastDamage = 0f;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        // Calculăm factorul de intensitate al Vignette-ului
        float intensity = Mathf.Clamp01((float)currentHealth / maxHealth);

        // Setăm noua intensitate a Vignette-ului
        vignette.intensity.value = intensity;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

	void IncreaseHealthOverTime()
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += 1;
			currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
			healthBar.SetHealth(currentHealth);
		}
	}

    public void SetHealth(int newHealth)
    {
        currentHealth = newHealth;
        healthBar.SetHealth(currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

	void Die()
    {
        Debug.Log("You died!");
    }

    void IncreaseHealth(int amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);
    }
}

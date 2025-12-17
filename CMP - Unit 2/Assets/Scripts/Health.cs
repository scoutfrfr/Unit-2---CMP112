using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Components
    private Animator anim;
    private AudioSource source;

    [Header("Health Variables")] 
    public float maxHealth = 5f;
    [HideInInspector]
    public float currentHealth; 

    [Header("Sound effects")] 
    public AudioClip damageSound;
    public AudioClip healthPickupSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set Variables
        currentHealth = maxHealth; 

        // Get Components
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Subtracts damage from players health and ensures players health never goes below zero 
        Debug.Log(currentHealth); // Prints players current health in console after they take damage (used for testing)

        if (currentHealth > 0) // if currentHealth is greater than zero and player is damaged play damaged sound effect and animation
        {
            anim.SetTrigger("Damaged");
            source.PlayOneShot(damageSound, 1.0f);
        }
        else // else (currentHealth <= 0) player dies
        {
            GetComponent<playerMovement>().Die();
            GetComponent<playerMovement>().enabled = false;
            source.PlayOneShot(damageSound, 1.0f);
        }
    }

    public void HealthRegen(int healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0, maxHealth); // Adds healthValue to players health and ensures health never goes above max health
        source.PlayOneShot(healthPickupSound, 1.0f);
    }

}

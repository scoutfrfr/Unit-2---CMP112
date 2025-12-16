using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator anim;
    private AudioSource source;

    public float maxHealth = 5f;
    public float currentHealth;
    public AudioClip damageSound;
    public AudioClip healthPickupSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Subtracts damage from players health and ensures players health never goes below zero 
        Debug.Log(currentHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Damaged");
            source.PlayOneShot(damageSound, 1.0f);
        }
        else
        {
            GetComponent<playerMovement>().Die();
            GetComponent<playerMovement>().enabled = false;
            source.PlayOneShot(damageSound, 1.0f);
        }
    }

    public void HealthRegen(int healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0, maxHealth);
        source.PlayOneShot(healthPickupSound, 1.0f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    // Declaring private variables 
    private Animator anim;


    // Declaring public variables 
    public float maxHealth;
    public float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Sets players health to max health at start of game
        anim = GetComponent<Animator>(); // Calls the animator component
    }

    public void TakeDamage(float damage) // Function to take damage
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Subtracts damage from players health and ensures players health never goes below zero 
        Debug.Log(currentHealth); // Shows players current health in console

        if (currentHealth > 0) 
        {
            anim.SetTrigger("Damaged"); // Plays damaged animation
        }
        else
        {
            GetComponent<playerMovement>().Die(); // Calls die function when playersHealth is not greater than zero
        }
    }
}

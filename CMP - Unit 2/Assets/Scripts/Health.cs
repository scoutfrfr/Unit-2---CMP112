using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator anim;

    public float maxHealth = 5f;
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Subtracts damage from players health and ensures players health never goes below zero 
        Debug.Log(currentHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Damaged");
        }
        else
        {
            GetComponent<playerMovement>().Die();
            GetComponent<playerMovement>().enabled = false;
        }
    }
}

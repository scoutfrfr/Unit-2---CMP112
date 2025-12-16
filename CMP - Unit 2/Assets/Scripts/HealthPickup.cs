using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Declaring public variables
    [HideInInspector] // Hides healthValue in inspector as it does not need to be accessed there.
    public int healthValue;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Checks if object collided with has tag "Player"
        {
            int healthValue = Random.Range(1, 4); // Generates random number 1-3, this will be the amount of health the player gets back from the health pickup.
            Debug.Log(healthValue); // Prints value of healthValue in console (used to test it worked)
            collision.GetComponent<Health>().HealthRegen(healthValue); // Calls HealthRegen function from Health script
            gameObject.SetActive(false); // Sets object as false so it can't be collected multiple times
            
        }
    }
}

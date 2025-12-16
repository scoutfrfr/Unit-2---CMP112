using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthValue;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int healthValue = Random.Range(1, 4); // Generates random number 1-3, this will be the amount of health the player gets back from the health pickup.
            Debug.Log(healthValue);
            collision.GetComponent<Health>().HealthRegen(healthValue);
            gameObject.SetActive(false);
            
        }
    }
}

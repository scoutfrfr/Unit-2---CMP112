using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    [Header("Enemy Dmg Amount")]
    public float damage;


    private void OnTriggerEnter2D(Collider2D collision) // if player collides with enemy (spikes, acid, out of bounds) player takes damage
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage); // Calls TakeDamage function from Health script
        }
    }


}

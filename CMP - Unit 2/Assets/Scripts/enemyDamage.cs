using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }


}

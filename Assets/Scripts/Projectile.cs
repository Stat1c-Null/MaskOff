using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(15f);
            Destroy(gameObject);
        }
    }
}

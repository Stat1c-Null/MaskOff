using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(15f);
            Debug.Log("Enemy hit by projectile!");
            Destroy(gameObject);
        }
    }
}

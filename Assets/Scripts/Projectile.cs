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
        /*if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().canGetHit == true && player.GetComponent<PlayerController>().isAttackActive == false)
            {
                collision.gameObject.GetComponent<PlayerController>().Hit();
                Debug.Log("Player hit by projectile!");
            }      
            Destroy(gameObject);
        }*/
    }
}

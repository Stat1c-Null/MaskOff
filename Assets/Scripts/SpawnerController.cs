using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject meleeEnemy;
    [SerializeField] private GameObject rangedEnemy;

    public int spawnMeleeCount = 2; //may need to be on gameController or separate script? For multiple spawners
    public int spawnRangedCount = 1;
    public float spawnRadius = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            for (int i = 0; i < spawnMeleeCount; i++)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + (i * spawnRadius + spawnRadius), transform.position.y - 1.3f, 0);
                GameObject newMelee = Instantiate(meleeEnemy, spawnPosition, Quaternion.identity);
            }

            for (int i = 0; i < spawnRangedCount; i++)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + (i * spawnRadius + (spawnRadius + 5)), transform.position.y - 1.1f, 0);
                GameObject newMelee = Instantiate(rangedEnemy, spawnPosition, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }
}

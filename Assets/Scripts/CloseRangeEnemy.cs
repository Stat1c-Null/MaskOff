using UnityEngine;

public class CloseRangeEnemy : Enemy
{

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Close Range Enemy spawned with health: " + health);
        Debug.Log("Close Range Enemy speed: " + speed);
        currentState = State.Wandering;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < aggroRange)
        {
            Debug.Log("Player in range! Attacking!");
            currentState = State.Attacking;
        }
        else
        {
            Debug.Log("Player out of range. Wandering.");
            currentState = State.Wandering;
        }

        //Switch behaviour based on currentState
        if (currentState == State.Wandering)
        {
            Wander();
        }
        else if (currentState == State.Attacking)
        {
            MeleeAttack();
        }
    }

    void Wander()
    {
        // Implementation for wandering behavior
    }

    void MeleeAttack()
    {
        // Implementation for melee attack behavior
    }
}

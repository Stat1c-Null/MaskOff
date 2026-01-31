using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloseRangeEnemy : Enemy
{

    public GameObject player;
    bool choosingDirection = false;
    public float wanderWait = 2f;
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
            if (!choosingDirection)
            {
                choosingDirection = true;
                StartCoroutine(Wander());
            }
        }
        else if (currentState == State.Attacking)
        {
            MeleeAttack();
        }
    }

    IEnumerator Wander()
    {
        // Choose a random direction to move in
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        // Move in that direction for a short duration
        float wanderDuration = Random.Range(5f, 10f);
        float elapsed = 0f;
        while (elapsed < wanderDuration)
        {
            transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Wait(wanderWait));
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        choosingDirection = false;
    }

    void MeleeAttack()
    {
        // Implementation for melee attack behavior
    }
}

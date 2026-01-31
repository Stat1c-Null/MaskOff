using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloseRangeEnemy : Enemy
{
    // Update is called once per frame
    void Update()
    {
        //Calculate distance to player
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < aggroRange && currentState != State.Attacking)
        {
            Debug.Log("Player in range! Attacking!");
            currentState = State.Attacking;
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
            MoveTowards(player.transform.position);
        }
    }

    void MeleeAttack()
    {
        if (player.GetComponent<PlayerController>().canGetHit == true)
        {
            Debug.Log("Attacking the player with a melee attack!");
            player.GetComponent<PlayerController>().Hit();
        }
        
    }

    void MoveTowards(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) <= attackDistance)
        {
            MeleeAttack();
            return;
        } else {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * attackMoveSpeed * Time.deltaTime, Space.World);
        }
    }
}

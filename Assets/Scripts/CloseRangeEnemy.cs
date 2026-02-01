using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloseRangeEnemy : Enemy
{   
    private bool canAttack = true;
    [SerializeField] private float attackCooldown = 1.5f;

    // Update is called once per frame
    void Update()
    {
        //Calculate distance to player  
        distanceToPlayer = Vector3.Distance(tf.position, player.GetComponent<Transform>().position);
        if (distanceToPlayer < aggroRange && currentState != State.Attacking)
        {
            //Debug.Log("Player in range! Attacking!");
            currentState = State.Attacking;
        }

        //Switch behaviour based on currentState
        if (currentState == State.Wandering)
        {
            anim.Play("CloseIdle");
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
        if (canAttack && player.GetComponent<PlayerController>().canGetHit == true)
        {
            Debug.Log("Attacking the player with a melee attack!");
            canAttack = false;
            anim.SetBool("Attacking", true);
            //Change attack directions
            if (currentDirection == Direction.Left)
            {
                tf.localScale = new Vector3(1f, 1f, 1f);   
            } else if (currentDirection == Direction.Right) {
                tf.localScale = new Vector3(-1f, 1f, 1f);
            }
            player.GetComponent<PlayerController>().Hit();
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void MoveTowards(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) <= attackDistance && canTakeDamage == true)
        {
            MeleeAttack();
            return;
        } else {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * attackMoveSpeed * Time.deltaTime, Space.World);
            anim.SetBool("Attacking", false);
            
            // Flip sprite based on movement direction
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                currentDirection = Direction.Left;
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                currentDirection = Direction.Right;
            }
        }
    }
}

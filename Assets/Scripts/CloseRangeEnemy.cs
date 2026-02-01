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
        // Don't process when dialog is active
        if (DialogController.IsGamePaused)
            return;

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
        anim.Play("CloseAttack");
        if (canAttack && player.GetComponent<PlayerController>().canGetHit == true)
        {
            Debug.Log("Attacking the player with a melee attack!");
            canAttack = false;
            anim.SetBool("Attacking", true);
            // Face the player based on the position
            if (player.transform.position.x > tf.position.x)
            {
                tf.localScale = new Vector3(1f, 1f, 1f);
                currentDirection = Direction.Right;
            }
            else
            {
                tf.localScale = new Vector3(-1f, 1f, 1f);
                currentDirection = Direction.Left;
            }

            if (player.GetComponent<PlayerController>().blocking )
            {
                player.GetComponent<PlayerController>().Block();
                StartCoroutine(AttackCooldown());
            }
            else {
                player.GetComponent<PlayerController>().Hit();
                StartCoroutine(AttackCooldown());
                TakeDamage(10);
            }
                
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
                transform.localScale = new Vector3(1f, 1f, 1f);
                currentDirection = Direction.Right;
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                currentDirection = Direction.Left;
            }
        }
    }
}

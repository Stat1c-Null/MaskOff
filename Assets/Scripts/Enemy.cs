using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        CloseRange,
        Ranged,
        Boss
    }

    public enum State 
    {
        Wandering,
        Attacking,
    }
    public EnemyType enemyType;
    public State currentState;

    [SerializeField] protected float health;
    [SerializeField] protected float wanderMoveSpeed;
    [SerializeField] protected float attackMoveSpeed;
    [SerializeField] protected float aggroRange;
    [SerializeField] protected float attackDistance;
    protected bool choosingDirection = false;
    [SerializeField] protected float wanderWait = 2f;

    protected IEnumerator Wander()
    {
        // Choose a random direction to move in
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        // Move in that direction for a short duration
        float wanderDuration = Random.Range(5f, 10f);
        float elapsed = 0f;
        while (elapsed < wanderDuration)
        {
            transform.Translate(randomDirection * wanderMoveSpeed * Time.deltaTime, Space.World);
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Wait(wanderWait));
    }

    protected IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        choosingDirection = false;
    }
    
}

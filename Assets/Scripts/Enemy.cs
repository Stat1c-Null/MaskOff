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
    protected  bool choosingDirection = false;
    [SerializeField] protected float wanderWait = 2f;
    [SerializeField] protected GameObject player;
    protected Transform tf;
    protected float distanceToPlayer;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected float damageCooldown = 0.8f;
    protected bool canTakeDamage = true;
    [SerializeField] private float rageIncrease;
    public enum Direction
    {
        Left,
        Right
    }

    protected Direction currentDirection = Direction.Left;

    void Start()
    {
        tf = GetComponent<Transform>();
        currentState = State.Wandering;
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == null)
        {
            Debug.LogError("Player object not found in the scene. Please ensure there is a GameObject with the tag 'Player'.");
        }
        anim = GetComponent<Animator>();
    }

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

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
            return;
        health -= damage;
        canTakeDamage = false;

        StartCoroutine(DamageCooldown());
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy died.");
        player.GetComponent<PlayerController>().IncreaseRage(rageIncrease);
        Destroy(gameObject);
    }

    private IEnumerator DamageCooldown()
    {
        //Flash effect
        // Set alpha low
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
        
        // Restore alpha
        color.a = 1f;
        spriteRenderer.color = color;
    }
}

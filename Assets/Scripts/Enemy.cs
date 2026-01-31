using UnityEngine;

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

    public float health;
    public float speed;

    public float aggroRange;
}

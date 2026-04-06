using UnityEngine;

public class UnitAI2D : MonoBehaviour
{
    public enum UnitState { Idle, Move, Combat }

    [Header("Settings")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1.2f;

    [Header("Targets")]
    public Transform targetBase; 
    private Transform currentEnemy;

    private UnitState currentState = UnitState.Move;
    private float lastAttackTime;

    void Update()
    {
        FSM();
    }

    private void FSM()
    {
        switch (currentState)
        {
            case UnitState.Move:
                HandleMoveState();
                break;
            case UnitState.Combat:
                HandleCombatState();
                break;
        }
    }

    private void HandleMoveState()
    {
        // 2D용 원형 탐색
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            currentEnemy = hit.transform;
            currentState = UnitState.Combat;
            return;
        }

        if (targetBase != null)
        {
            // 2D 이동 (X축 중심)
            Vector2 direction = (targetBase.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleCombatState()
    {
        if (currentEnemy == null)
        {
            currentState = UnitState.Move;
            return;
        }

        float distanceToEnemy = Vector2.Distance(transform.position, currentEnemy.position);

        if (distanceToEnemy > attackRange)
        {
            Vector2 direction = (currentEnemy.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log(gameObject.name + "가 2D 공격을 시도합니다!");
            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

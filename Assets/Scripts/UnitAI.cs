using UnityEngine;

public class UnitAI : MonoBehaviour
{
    public enum UnitState { Idle, Move, Combat }

    [Header("Settings")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;

    [Header("Targets")]
    public Transform targetBase; // 적 기지
    private Transform currentEnemy; // 현재 감지된 적 유닛

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
            case UnitState.Idle:
                // 대기 상태 로직 (필요 시)
                break;
        }
    }

    private void HandleMoveState()
    {
        // 1. 적 유닛 탐색 (반경 5 이내 'Enemy' 태그)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                currentEnemy = hit.transform;
                currentState = UnitState.Combat;
                return;
            }
        }

        // 2. 적 기지를 향해 이동
        if (targetBase != null)
        {
            Vector3 targetPos = new Vector3(targetBase.position.x, transform.position.y, targetBase.position.z);
            Vector3 direction = (targetPos - transform.position).normalized;
            
            transform.position += direction * moveSpeed * Time.deltaTime;
            if (direction != Vector3.zero)
                transform.forward = direction; // 전진 방향 바라보기
        }
    }

    private void HandleCombatState()
    {
        // 적이 사라졌거나 죽었을 경우 이동 상태로 복귀
        if (currentEnemy == null || !currentEnemy.gameObject.activeInHierarchy)
        {
            currentEnemy = null;
            currentState = UnitState.Move;
            return;
        }

        float distanceToEnemy = Vector3.Distance(transform.position, currentEnemy.position);

        // 사거리 밖이면 적에게 접근
        if (distanceToEnemy > attackRange)
        {
            Vector3 targetPos = new Vector3(currentEnemy.position.x, transform.position.y, currentEnemy.position.z);
            Vector3 direction = (targetPos - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            // 사거리 안이면 공격 루틴
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log(gameObject.name + "이(가) " + currentEnemy.name + "을(를) 공격합니다!");
            
            // 여기에 실제 데미지 입히는 로직 추가 가능
            // currentEnemy.GetComponent<Health>().TakeDamage(damage);

            lastAttackTime = Time.time;
        }
    }

    // 에디터에서 사거리 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log(gameObject.name + " HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 실제 게임에서는 오브젝트 풀링을 쓰는 게 좋지만, 일단 파괴로 구현
        Destroy(gameObject);
    }
}

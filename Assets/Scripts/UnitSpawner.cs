using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Prefab")]
    public GameObject unitPrefab;
    public Transform spawnPoint;
    public Transform enemyBase; // 소환된 유닛이 갈 곳
    public float unitCost = 20f;

    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
    }

    // 버튼 클릭 시 호출될 함수
    public void SpawnUnit()
    {
        if (resourceManager != null && resourceManager.CanAfford(unitCost))
        {
            resourceManager.SpendResources(unitCost);
            
            // 유닛 생성
            GameObject newUnit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            
            // 유닛 AI 설정
            UnitAI ai = newUnit.GetComponent<UnitAI>();
            if (ai != null)
            {
                ai.targetBase = enemyBase;
            }

            Debug.Log("Unit Spawned! Remaining Resources: " + resourceManager.currentResources);
        }
        else
        {
            Debug.Log("Not enough resources!");
        }
    }
}

using UnityEngine;
using TMPro; // 텍스트 표시를 위해 필요 (TextMeshPro)

public class ResourceManager : MonoBehaviour
{
    [Header("Resource Settings")]
    public float currentResources = 0f;
    public float resourcePerSecond = 2f; // 초당 2씩 증가
    public float maxResources = 100f;

    [Header("UI Reference")]
    public TextMeshProUGUI resourceText; // 유니티 에디터에서 연결

    void Update()
    {
        GenerateResources();
        UpdateUI();
    }

    void GenerateResources()
    {
        if (currentResources < maxResources)
        {
            currentResources += resourcePerSecond * Time.deltaTime;
            // 최대치 초과 방지
            currentResources = Mathf.Min(currentResources, maxResources);
        }
    }

    void UpdateUI()
    {
        if (resourceText != null)
        {
            // 소수점 제외하고 정수로 표시
            resourceText.text = "Resources: " + Mathf.FloorToInt(currentResources).ToString();
        }
    }

    // 유닛 소환 시 자원 소모 체크용 함수
    public bool CanAfford(float amount)
    {
        return currentResources >= amount;
    }

    public void SpendResources(float amount)
    {
        currentResources -= amount;
    }
}

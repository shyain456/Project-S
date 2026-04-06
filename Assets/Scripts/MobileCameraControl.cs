using UnityEngine;

public class MobileCameraControl : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float minX = 0f;      // 우리팀 본진 위치
    public float maxX = 100f;    // 적군 본진 위치

    private Vector2 lastTouchPos;

    void Update()
    {
        // 1. 모바일 터치 입력 감지 (1개 손가락)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // 드래그 거리에 따른 카메라 이동 (X축 전진/후진만 가능하도록 고정)
                float deltaX = (touch.position.x - lastTouchPos.x) * scrollSpeed;
                
                Vector3 newPos = transform.position;
                newPos.x -= deltaX; // 드래그 방향에 맞춰 카메라 이동
                
                // 전장 범위 밖으로 나가지 않도록 제한(Clamp)
                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
                
                transform.position = newPos;
                lastTouchPos = touch.position;
            }
        }
    }
}

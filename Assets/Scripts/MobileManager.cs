using UnityEngine;

public class MobileManager : MonoBehaviour
{
    void Awake()
    {
        // 1. 가로 모드 고정 (Landscape Left & Right 허용)
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        // 2. 모바일 배터리 및 발열을 고려한 프레임 레이트 제한 (60fps)
        Application.targetFrameRate = 60;

        // 3. 화면 꺼짐 방지
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}

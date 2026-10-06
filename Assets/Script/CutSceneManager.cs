using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance; // 어디서든 부를 수 있는 싱글톤

    public GameObject cutsceneUI;   // 컷씬 패널 연결
    public VideoPlayer videoPlayer; // 비디오 플레이어 연결

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // 게임 시작 시 패널은 무조건 꺼둠
        if (cutsceneUI != null) cutsceneUI.SetActive(false);
    }

    // 영상 재생 코루틴
    public IEnumerator PlayCutsceneRoutine()
    {
        cutsceneUI.SetActive(true); // 패널 켜기
        videoPlayer.Play();         // 영상 재생

        // 영상 길이만큼 대기
        yield return new WaitForSeconds((float)videoPlayer.length);

        videoPlayer.Stop();         // 영상 정지
        cutsceneUI.SetActive(false);// 패널 다시 끄기
    }
}
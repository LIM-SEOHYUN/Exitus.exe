using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStage = 1;
    public int successCount = 0;

    public Transform spawnPoint;
    public Transform player;

    public StageUI stageUI;

    public Image fadePanel;
    public float fadeDuration = 0.5f;
    public TMPro.TMP_Text resultText;

    private void MovePlayerToSpawn()
    {
        player.position = spawnPoint.position;
    }

    void Start()
    {
        MovePlayerToSpawn();
        AnomalyManager.Instance.DecideStageAnomaly();

        stageUI.UpdateStage(currentStage);
        stageUI.UpdateSuccess(successCount);

        StartCoroutine(FadeIn());
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Success()
    {
        if (successCount >= 8)
        {
            return;
        }
        StartCoroutine(SuccessWithFade());
    }

    private IEnumerator SuccessWithFade()
    {
        // 화면 암전
        yield return StartCoroutine(FadeOut());

        resultText.text = "CORRECT";
        resultText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        resultText.gameObject.SetActive(false);

        // 이상현상이 있는 스테이지를 정답 처리한 경우 도감 등록
        if (AnomalyManager.Instance.CurrentStageHasAnomaly)
        {
            AnomalyDexManager.Instance.Unlock(AnomalyManager.Instance.CurrentAnomaly);
        }
        
        currentStage++;
        successCount++;

        stageUI.UpdateStage(currentStage);
        stageUI.UpdateSuccess(successCount);

        AnomalyManager.Instance.DecideStageAnomaly();

        MovePlayerToSpawn();

        Debug.Log("성공");

        if (successCount >= 8)
        {
            Debug.Log("게임 클리어!");
            yield return StartCoroutine(FadeIn());
            yield break;
        }

        Debug.Log("현재 스테이지 : " + currentStage);
        Debug.Log("연속 성공 : " + successCount);

        // 다음 라운드 보여주기
        yield return StartCoroutine(FadeIn());
    }

    public void Fail()
    {
        StartCoroutine(FailWithFade());
    }

    private IEnumerator FailWithFade()
    {
        // 화면 암전
        yield return StartCoroutine(FadeOut());

        resultText.text = "WRONG";
        resultText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        
        resultText.text = "RESET";

        yield return new WaitForSeconds(0.8f);

        resultText.gameObject.SetActive(false);

        currentStage = 1;
        successCount = 0;

        stageUI.UpdateStage(currentStage);
        stageUI.UpdateSuccess(successCount);

        AnomalyManager.Instance.ResetUsedAnomalies();
        AnomalyManager.Instance.DecideStageAnomaly();

        MovePlayerToSpawn();

        Debug.Log("실패");
        Debug.Log("게임 리셋");
        Debug.Log("현재 스테이지 : " + currentStage);
        Debug.Log("연속 성공 : " + successCount);

        // 화면 밝아짐
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;

        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;

        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;
    }
}
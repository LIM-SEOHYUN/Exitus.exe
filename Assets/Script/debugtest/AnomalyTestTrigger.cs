using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyTestTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AnomalyManager.Instance.DecideStageAnomaly();

            if (AnomalyManager.Instance.CurrentStageHasAnomaly)
            {
                Debug.Log($"이상현상 발생: {AnomalyManager.Instance.CurrentAnomaly.AnomalyId}");
            }
            else
            {
                Debug.Log("이번엔 이상현상 없음");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AnomalyManager.Instance.ResetUsedAnomalies();
            Debug.Log("사용 기록 초기화됨");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (AnomalyManager.Instance.CurrentAnomaly == null)
            {
                Debug.LogWarning("해금 실패: 현재 발동된 이상현상이 없음 (먼저 T를 눌러 이상현상을 발생시켜야 함)");
                return;
            }

            AnomalyDexManager.Instance.Unlock(AnomalyManager.Instance.CurrentAnomaly);
            Debug.Log($"도감 해금 시도: {AnomalyManager.Instance.CurrentAnomaly.DisplayName}");
        }
    }
}
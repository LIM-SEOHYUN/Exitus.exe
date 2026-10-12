using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance { get; private set; }

    [Header("이상현상 풀")]
    [SerializeField] private List<AnomalyBase> anomalyPool;

    [Header("발생 확률")]
    [Range(0f, 1f)][SerializeField] private float anomalyChance = 0.75f;

    private HashSet<string> usedAnomalyIds = new HashSet<string>();

    public bool CurrentStageHasAnomaly { get; private set; }
    public AnomalyBase CurrentAnomaly { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //이상현상 유무 및 이상현상 종류 선택
    // 스테이지 시작 시 StageController가 호출해야 함
    public void DecideStageAnomaly()
    {
        if (CurrentAnomaly != null)
        {
            if (CurrentAnomaly is MonsterChaseAnomaly monsterAnomaly)
            {
                monsterAnomaly.ClearMonster();
            }
            CurrentAnomaly = null;
        }
        List<AnomalyBase> availablePool = anomalyPool.FindAll(a => !usedAnomalyIds.Contains(a.AnomalyId));

        if (availablePool.Count == 0 || Random.value > anomalyChance)
        {
            CurrentStageHasAnomaly = false;
            CurrentAnomaly = null;
            return;
        }

        CurrentStageHasAnomaly = true;
        CurrentAnomaly = availablePool[Random.Range(0, availablePool.Count)];
        usedAnomalyIds.Add(CurrentAnomaly.AnomalyId);

        CurrentAnomaly.Activate();
    }

    //이상현상 풀 초기화
    // 1스테이지로 초기화될 때 GameManager가 호출해야함
    public void ResetUsedAnomalies()
    {
        usedAnomalyIds.Clear();
    }
    // 도감 UI의 "발견 개수 / 전체 개수" 표시용
    public int GetTotalAnomalyCount() => anomalyPool.Count;
}
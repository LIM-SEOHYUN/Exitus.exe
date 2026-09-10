using System.Collections.Generic;
using UnityEngine;

public class AnomalyDexManager : MonoBehaviour
{
    public static AnomalyDexManager Instance { get; private set; }

    [System.Serializable]
    public class DexEntry
    {
        public string anomalyId;
        public string displayName;
        public string description;
    }

    // anomalyId를 key로 써서 중복 해금을 자동으로 막음
    private Dictionary<string, DexEntry> unlockedEntries = new Dictionary<string, DexEntry>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 씬이 리로드되는 방식으로 라운드가 초기화되더라도
        // 도감 해금 정보는 살아있어야 하므로 파괴되지 않게 설정
        DontDestroyOnLoad(gameObject);
    }

    public void Unlock(AnomalyBase anomaly)
    {
        if (anomaly == null) return;
        if (unlockedEntries.ContainsKey(anomaly.AnomalyId)) return; // 이미 해금됨

        unlockedEntries[anomaly.AnomalyId] = new DexEntry
        {
            anomalyId = anomaly.AnomalyId,
            displayName = anomaly.DisplayName,
            description = anomaly.Description
        };

        Debug.Log($"도감 해금: {anomaly.DisplayName}");
    }

    public bool IsUnlocked(string anomalyId) => unlockedEntries.ContainsKey(anomalyId);

    public List<DexEntry> GetUnlockedEntries() => new List<DexEntry>(unlockedEntries.Values);

    public int GetUnlockedCount() => unlockedEntries.Count;
}
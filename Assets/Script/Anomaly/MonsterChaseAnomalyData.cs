using UnityEngine;

[CreateAssetMenu(menuName = "Anomaly/MonsterChaseAnomalyData")]
public class MonsterChaseAnomalyData : AnomalyData
{
    public GameObject monsterPrefab;   // 소환할 몬스터 프리팹
    //public string spawnPointId;       // 몬스터가 나타날 위치
    public float chaseSpeed;           // 추적 속도
}

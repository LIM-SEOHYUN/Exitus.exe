using UnityEngine;

public class MonsterChaseAnomaly : AnomalyBase
{
    private MonsterChaseAnomalyData MyData => data as MonsterChaseAnomalyData;
    private GameObject spawnedMonster;

    // 씬에서 직접 연결할 Transform (스폰 위치)
    public Transform spawnPoint;

    public override void Activate()
    {
        if (MyData == null)
        {
            Debug.LogError("MonsterChaseAnomalyData 연결 안됨");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point 연결 안됨");
            return;
        }

        // 몬스터 소환
        spawnedMonster = Instantiate(MyData.monsterPrefab, spawnPoint.position, Quaternion.identity);

        // 몬스터 AI에 속도 전달
        CreatureAI ai = spawnedMonster.GetComponent<CreatureAI>();
        ai.moveSpeed = MyData.chaseSpeed;

        // 컷씬 UI 주입 (씬에 있는 오브젝트 연결)
        //ai.cutsceneUI = GameObject.Find("CreatureTriggerCutScene");

        // 한 프레임 기다렸다가 Activate 실행 (Start()가 먼저 돌도록 보장)
        StartCoroutine(ActivateAfterFrame(ai));
    }

    // 코루틴: 한 프레임 대기 후 Activate 호출
    private System.Collections.IEnumerator ActivateAfterFrame(CreatureAI ai)
    {
        yield return null; // 한 프레임 대기
        ai.Activate();
    }

    public override bool IsResolved()
    {
        // 몬스터가 아직 살아있으면 스테이지 클리어 보류
        return spawnedMonster == null;
    }
}

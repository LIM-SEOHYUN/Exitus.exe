using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnomalyData", menuName = "Anomaly/AnomalyData")]
public class AnomalyData : ScriptableObject
{
    [Header("식별 정보")]
    [SerializeField] private string anomalyId; //이상현상 아이디
    [SerializeField] private string displayName; //이상현상 이름
    [TextArea][SerializeField] private string description; //이상현상 정보->텍스트로 이게 어떤 이상현상인지 다른 팀원이 알수 있는 내용

    //외부에서 수정 안되게 대문자로 시작하는 변수로 읽기만 가능하게 제한함
    public string AnomalyId => anomalyId; //예시: AnomalyId -> 상속받은 외부에서 참조 가능, anomalyId = 수정 불가능
    public string DisplayName => displayName;
    public string Description => description;
}

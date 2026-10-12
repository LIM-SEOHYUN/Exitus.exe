using UnityEngine;

public class PanelImageAnomaly : AnomalyBase
{
    public override void Activate()
    {
        PanelImageAnomalyData panelData =
            data as PanelImageAnomalyData;

        if (panelData == null)
        {
            Debug.LogError(
                "[PanelImageAnomaly] 데이터 타입이 잘못되었습니다."
            );
            return;
        }

        if (panelData.anomalyImage == null)
        {
            Debug.LogError(
                "[PanelImageAnomaly] 기괴한 이미지가 연결되지 않았습니다."
            );
            return;
        }

        PanelImageInteractable[] targets =
            FindObjectsOfType<PanelImageInteractable>();

        foreach (PanelImageInteractable target in targets)
        {
            if (target.entityId == panelData.targetEntityId)
            {
                target.ActivateAnomaly(panelData.anomalyImage);

                Debug.Log(
                    "[PanelImageAnomaly] 이상현상 활성화 성공: "
                    + panelData.targetEntityId
                );

                return;
            }
        }

        Debug.LogWarning(
            "[PanelImageAnomaly] 이상현상 대상을 찾지 못했습니다: "
            + panelData.targetEntityId
        );
    }
}
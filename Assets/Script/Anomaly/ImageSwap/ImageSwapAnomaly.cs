using UnityEngine;

public class ImageSwapAnomaly : AnomalyBase
{
    public override void Activate()
    {
        // AnomalyBase의 data를
        // ImageSwapAnomalyData로 변환
        ImageSwapAnomalyData imageSwapData =
            data as ImageSwapAnomalyData;


        if (imageSwapData == null)
        {
            Debug.LogError(
                "[ImageSwapAnomaly] Data가 ImageSwapAnomalyData가 아닙니다."
            );

            return;
        }


        // Scene에 있는 모든 InteractableEntity 검색
        InteractableEntity[] entities =
            FindObjectsOfType<InteractableEntity>();


        foreach (InteractableEntity entity in entities)
        {
            // Data에 지정된 ID와
            // 실제 오브젝트의 Entity ID 비교
            if (entity.entityId == imageSwapData.targetEntityId)
            {
                // 실제 이미지 스왑 이상현상 활성화
                entity.ActivateImageSwap(
                    imageSwapData.swappedSprite,
                    imageSwapData.swapChance
                );


                Debug.Log(
                    "[ImageSwapAnomaly] 대상 활성화 성공: "
                    + imageSwapData.targetEntityId
                );


                return;
            }
        }


        Debug.LogWarning(
            "[ImageSwapAnomaly] 대상 Entity ID를 찾지 못했습니다: "
            + imageSwapData.targetEntityId
        );
    }
}
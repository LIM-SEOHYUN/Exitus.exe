using UnityEngine;

public class ImageSwapAnomaly : AnomalyBase
{
    public override void Activate()
    {
        // AnomalyBase의 data를
        // 우리가 만든 ImageSwapAnomalyData 타입으로 변환
        ImageSwapAnomalyData imageSwapData = data as ImageSwapAnomalyData;

        // 데이터가 제대로 연결되어 있지 않은 경우
        if (imageSwapData == null)
        {
            Debug.LogError(
                "[ImageSwapAnomaly] Data가 ImageSwapAnomalyData가 아닙니다."
            );

            return;
        }

        // 현재 Scene에 존재하는 모든
        // InteractableEntity를 찾음
        InteractableEntity[] entities =
            FindObjectsOfType<InteractableEntity>();

        // 하나씩 확인
        foreach (InteractableEntity entity in entities)
        {
            // ImageSwapAnomalyData에 설정한 Target Entity ID와
            // 실제 오브젝트의 Entity ID가 같은지 확인
            if (entity.entityId == imageSwapData.targetEntityId)
            {
                // 기존 이미지 스왑 이상현상 활성화
                entity.ActivateImageSwap(
                    imageSwapData.swappedSprite,
                    imageSwapData.swapChance
                );

                Debug.Log(
                    "[ImageSwapAnomaly] 이미지 스왑 이상현상 활성화: "
                    + imageSwapData.targetEntityId
                );

                // ================================
                // E키 표시 기능 추가 부분
                // ================================

                // 같은 오브젝트에 붙어 있는
                // ImageSwapPrompt 컴포넌트를 찾음
                ImageSwapPrompt prompt =
                    entity.GetComponent<ImageSwapPrompt>();

                // ImageSwapPrompt가 존재한다면
                if (prompt != null)
                {
                    // 이제 플레이어가 가까이 왔을 때
                    // [E] 표시를 띄울 수 있도록 활성화
                    prompt.EnablePrompt();

                    Debug.Log(
                        "[ImageSwapAnomaly] E키 상호작용 표시 활성화: "
                        + imageSwapData.targetEntityId
                    );
                }
                else
                {
                    Debug.LogWarning(
                        "[ImageSwapAnomaly] 대상 오브젝트에 ImageSwapPrompt가 없습니다: "
                        + imageSwapData.targetEntityId
                    );
                }

                // 대상 하나를 찾았으므로 종료
                return;
            }
        }

        // ID가 일치하는 오브젝트를 못 찾았을 경우
        Debug.LogWarning(
            "[ImageSwapAnomaly] 대상 ID를 가진 InteractableEntity를 찾지 못했습니다: "
            + imageSwapData.targetEntityId
        );
    }
}
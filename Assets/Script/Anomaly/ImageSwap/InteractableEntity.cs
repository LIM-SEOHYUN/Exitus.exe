using UnityEngine;

public class InteractableEntity : MonoBehaviour
{
    [Header("Entity ID")]
    public string entityId;

    [Header("Image")]
    public SpriteRenderer spriteRenderer;

    [Header("Test Only")]
    public Sprite interactionTestSprite;


    // 처음 가지고 있던 정상 이미지
    private Sprite originalSprite;

    // 이상현상 발동 시 바뀔 이미지
    private Sprite anomalySwapSprite;

    // 이미지 교체 확률
    private float anomalySwapChance = 1f;

    // 현재 이미지 스왑 이상현상이 활성화되어 있는지
    private bool imageSwapActive = false;

    // 이미 이미지가 변경되었는지
    private bool alreadySwapped = false;


    private void Awake()
    {
        // Inspector에 Sprite Renderer를 직접 넣지 않았을 경우
        // 현재 오브젝트에서 자동으로 찾음
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 정상 상태의 원본 이미지를 저장
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogWarning(
                "[InteractableEntity] SpriteRenderer가 없습니다: "
                + gameObject.name
            );
        }
    }


    // =====================================================
    // ImageSwapAnomaly가 발동했을 때 호출되는 함수
    // =====================================================
    public void ActivateImageSwap(Sprite swapSprite, float swapChance)
    {
        // 바뀔 이미지 저장
        anomalySwapSprite = swapSprite;

        // 확률 저장
        anomalySwapChance = Mathf.Clamp01(swapChance);

        // 이제 이 오브젝트에서 이미지 스왑을 할 수 있음
        imageSwapActive = true;

        // 아직 변경되지 않은 상태로 설정
        alreadySwapped = false;

        Debug.Log(
            "[InteractableEntity] 이미지 스왑 이상현상 활성화: "
            + entityId
        );
    }


    // =====================================================
    // 현재 플레이어가 이 오브젝트와
    // 상호작용할 수 있는 상태인지 확인
    // =====================================================
    public bool CanInteract()
    {
        return imageSwapActive && !alreadySwapped;
    }


    // =====================================================
    // PlayerInteraction에서 E키를 눌렀을 때 호출
    // =====================================================
    public void Interact()
    {
        // 이상현상이 활성화되지 않았다면 아무것도 하지 않음
        if (!imageSwapActive)
        {
            Debug.Log(
                "[InteractableEntity] 현재 활성화된 이미지 스왑 이상현상이 없습니다: "
                + entityId
            );

            return;
        }

        // 이미 한 번 이미지가 바뀌었다면 다시 실행하지 않음
        if (alreadySwapped)
        {
            Debug.Log(
                "[InteractableEntity] 이미 이미지가 변경된 대상입니다: "
                + entityId
            );

            return;
        }

        // SpriteRenderer 확인
        if (spriteRenderer == null)
        {
            Debug.LogWarning(
                "[InteractableEntity] SpriteRenderer가 없습니다: "
                + entityId
            );

            return;
        }

        // 바뀔 이미지 확인
        if (anomalySwapSprite == null)
        {
            Debug.LogWarning(
                "[InteractableEntity] 교체할 Sprite가 설정되지 않았습니다: "
                + entityId
            );

            return;
        }


        // =================================================
        // 이미지 변경 확률 판정
        // =================================================

        float randomValue = Random.value;

        if (randomValue > anomalySwapChance)
        {
            Debug.Log(
                "[InteractableEntity] 이미지 교체 확률 판정 실패: "
                + entityId
            );

            return;
        }


        // =================================================
        // 실제 이미지 변경
        // =================================================

        spriteRenderer.sprite = anomalySwapSprite;

        alreadySwapped = true;

        // 이상현상 처리가 끝났으므로 비활성화
        imageSwapActive = false;


        Debug.Log(
            "[InteractableEntity] 이미지 교체 성공: "
            + entityId
        );


        // =================================================
        // [E] 표시 제거
        // =================================================

        ImageSwapPrompt prompt =
            GetComponent<ImageSwapPrompt>();

        if (prompt != null)
        {
            prompt.DisablePrompt();
        }
    }


    // =====================================================
    // 원래 이미지로 되돌리는 함수
    // 필요할 경우 다른 시스템에서 호출 가능
    // =====================================================
    public void RestoreOriginalImage()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (originalSprite == null)
        {
            return;
        }

        // 원래 이미지 복구
        spriteRenderer.sprite = originalSprite;

        // 상태 초기화
        imageSwapActive = false;
        alreadySwapped = false;

        anomalySwapSprite = null;


        // E 표시도 제거
        ImageSwapPrompt prompt =
            GetComponent<ImageSwapPrompt>();

        if (prompt != null)
        {
            prompt.DisablePrompt();
        }


        Debug.Log(
            "[InteractableEntity] 원래 이미지로 복구: "
            + entityId
        );
    }
}
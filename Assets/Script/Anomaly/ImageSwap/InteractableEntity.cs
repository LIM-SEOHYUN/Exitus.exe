using UnityEngine;

public class InteractableEntity : MonoBehaviour
{
    [Header("Entity ID")]
    public string entityId;

    [Header("Image")]
    public SpriteRenderer spriteRenderer;

    [Header("Test Only")]
    public Sprite interactionTestSprite;


    // 원래 정상 이미지
    private Sprite originalSprite;

    // 실제 이상현상으로 바뀔 이미지
    private Sprite anomalySwapSprite;

    // 실제 이상현상의 이미지 변경 확률
    private float anomalySwapChance = 1f;

    // 실제 이미지 스왑 이상현상이 발동된 상태인가?
    private bool imageSwapActive = false;

    // 이미 한 번 그림이 변경되었는가?
    private bool alreadySwapped = false;


    private void Awake()
    {
        // Inspector에서 SpriteRenderer가 연결되지 않았다면
        // 자기 자신의 SpriteRenderer를 자동으로 찾음
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 정상 그림 저장
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
    // 실제 ImageSwap 이상현상이 발동됐을 때 호출
    // =====================================================
    public void ActivateImageSwap(Sprite swapSprite, float swapChance)
    {
        anomalySwapSprite = swapSprite;
        anomalySwapChance = Mathf.Clamp01(swapChance);

        imageSwapActive = true;
        alreadySwapped = false;

        Debug.Log(
            "[InteractableEntity] 이미지 스왑 이상현상 활성화: "
            + entityId
        );
    }


    // =====================================================
    // Player가 E키로 상호작용할 수 있는지 확인
    //
    // 1. 실제 이상현상이 활성화된 경우
    // 또는
    // 2. Interaction Test Sprite가 설정된 경우
    //
    // 둘 중 하나면 E 상호작용 가능
    // =====================================================
    public bool CanInteract()
    {
        if (alreadySwapped)
        {
            return false;
        }

        if (imageSwapActive)
        {
            return true;
        }

        // 예전에 사용하던 테스트 방식도 유지
        if (interactionTestSprite != null)
        {
            return true;
        }

        return false;
    }


    // =====================================================
    // [E] 표시를 띄워도 되는 상태인지 확인
    //
    // 중요:
    // Test Sprite가 있다고 [E]를 띄우는 게 아니라
    // 실제 이상현상이 발동한 경우에만 true
    // =====================================================
    public bool ShouldShowPrompt()
    {
        return imageSwapActive && !alreadySwapped;
    }


    // =====================================================
    // Player가 E키를 눌렀을 때 호출
    // =====================================================
    public void Interact()
    {
        if (alreadySwapped)
        {
            Debug.Log(
                "[InteractableEntity] 이미 그림이 변경되었습니다: "
                + entityId
            );

            return;
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning(
                "[InteractableEntity] SpriteRenderer가 없습니다: "
                + entityId
            );

            return;
        }


        Sprite spriteToUse = null;
        float chanceToUse = 1f;


        // -------------------------------------------------
        // 실제 이상현상이 발동한 경우
        // -------------------------------------------------
        if (imageSwapActive)
        {
            spriteToUse = anomalySwapSprite;
            chanceToUse = anomalySwapChance;
        }

        // -------------------------------------------------
        // 실제 이상현상이 발동하지 않았지만
        // 테스트용 Sprite가 있는 경우
        //
        // 예전에 E를 누르면 그림이 바뀌던 동작을 살리는 부분
        // -------------------------------------------------
        else if (interactionTestSprite != null)
        {
            spriteToUse = interactionTestSprite;
            chanceToUse = 1f;
        }

        else
        {
            Debug.Log(
                "[InteractableEntity] 현재 상호작용할 이미지가 없습니다: "
                + entityId
            );

            return;
        }


        // 교체할 이미지가 없는 경우
        if (spriteToUse == null)
        {
            Debug.LogWarning(
                "[InteractableEntity] 교체할 Sprite가 없습니다: "
                + entityId
            );

            return;
        }


        // -------------------------------------------------
        // 확률 판정
        // -------------------------------------------------

        float randomValue = Random.value;

        if (randomValue > chanceToUse)
        {
            Debug.Log(
                "[InteractableEntity] 이미지 교체 확률 판정 실패: "
                + entityId
            );

            return;
        }


        // -------------------------------------------------
        // 실제 이미지 변경
        // -------------------------------------------------

        spriteRenderer.sprite = spriteToUse;

        alreadySwapped = true;
        imageSwapActive = false;


        Debug.Log(
            "[InteractableEntity] 이미지 교체 성공: "
            + entityId
        );


        // -------------------------------------------------
        // 이미지 변경 후 [E] 표시 즉시 숨기기
        // -------------------------------------------------

        ImageSwapPrompt prompt =
            GetComponent<ImageSwapPrompt>();

        if (prompt != null)
        {
            prompt.HidePromptImmediately();
        }
    }


    // =====================================================
    // 원래 이미지로 되돌리기
    // =====================================================
    public void RestoreOriginalImage()
    {
        if (spriteRenderer != null && originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite;
        }

        imageSwapActive = false;
        alreadySwapped = false;
        anomalySwapSprite = null;


        ImageSwapPrompt prompt =
            GetComponent<ImageSwapPrompt>();

        if (prompt != null)
        {
            prompt.HidePromptImmediately();
        }


        Debug.Log(
            "[InteractableEntity] 원래 이미지로 복구: "
            + entityId
        );
    }
}
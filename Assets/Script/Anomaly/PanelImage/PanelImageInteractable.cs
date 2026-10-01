using UnityEngine;

public class PanelImageInteractable : MonoBehaviour
{
    [Header("Entity ID")]
    public string entityId;

    [Header("Interaction")]
    public KeyCode interactionKey = KeyCode.E;

    [Header("Panel")]
    public InspectPanelController panelController;

    [Header("Normal Image")]
    public Sprite normalImage;

    [Header("Test Only")]
    public bool forceAnomalyForTest = false;
    public Sprite testAnomalyImage;

    private bool playerInside = false;

    private bool anomalyActive = false;
    private Sprite anomalyImage;


    private void Update()
    {
        // 플레이어가 상호작용 영역 안에 있을 때만 E 사용
        if (!playerInside)
        {
            return;
        }

        // 이미 패널이 열려 있으면 다시 열지 않음
        if (panelController != null &&
            panelController.IsOpen)
        {
            return;
        }

        if (Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }


    // =====================================================
    // 플레이어가 상호작용 영역 안으로 들어옴
    // =====================================================
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction player =
            other.GetComponentInParent<PlayerInteraction>();

        if (player != null)
        {
            playerInside = true;

            Debug.Log(
                "[PanelImageInteractable] 상호작용 범위 진입: "
                + entityId
            );
        }
    }


    // =====================================================
    // 플레이어가 상호작용 영역 밖으로 나감
    // =====================================================
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInteraction player =
            other.GetComponentInParent<PlayerInteraction>();

        if (player != null)
        {
            playerInside = false;

            Debug.Log(
                "[PanelImageInteractable] 상호작용 범위 이탈: "
                + entityId
            );
        }
    }


    // =====================================================
    // E 상호작용
    // =====================================================
    private void Interact()
    {
        if (panelController == null)
        {
            Debug.LogError(
                "[PanelImageInteractable] Panel Controller가 없습니다."
            );

            return;
        }


        Sprite spriteToShow = null;


        // 테스트 이상현상
        if (forceAnomalyForTest &&
            testAnomalyImage != null)
        {
            spriteToShow = testAnomalyImage;
        }

        // 실제 이상현상
        else if (anomalyActive &&
                 anomalyImage != null)
        {
            spriteToShow = anomalyImage;
        }

        // 정상 상태
        else
        {
            spriteToShow = normalImage;
        }


        if (spriteToShow == null)
        {
            Debug.LogError(
                "[PanelImageInteractable] 표시할 이미지가 없습니다: "
                + entityId
            );

            return;
        }


        panelController.OpenPanel(spriteToShow);

        Debug.Log(
            "[PanelImageInteractable] 조사 성공: "
            + entityId
        );
    }


    // =====================================================
    // 이상현상 활성화
    // =====================================================
    public void ActivateAnomaly(Sprite newAnomalyImage)
    {
        anomalyImage = newAnomalyImage;
        anomalyActive = true;

        Debug.Log(
            "[PanelImageInteractable] 이상현상 활성화: "
            + entityId
        );
    }


    // =====================================================
    // 이상현상 초기화
    // =====================================================
    public void ResetAnomaly()
    {
        anomalyActive = false;
        anomalyImage = null;
    }
}
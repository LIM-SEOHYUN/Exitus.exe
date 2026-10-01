using UnityEngine;
using UnityEngine.UI;

public class InspectPanelController : MonoBehaviour
{
    [Header("Inspect Panel")]
    public GameObject inspectPanel;

    [Header("Inspect Image")]
    public Image inspectImage;


    // 현재 패널이 열려 있는지
    public bool IsOpen
    {
        get
        {
            return inspectPanel != null &&
                   inspectPanel.activeSelf;
        }
    }


    private void Awake()
    {
        if (inspectPanel == null)
        {
            Debug.LogError(
                "[InspectPanelController] InspectPanel이 연결되지 않았습니다."
            );
        }

        if (inspectImage == null)
        {
            Debug.LogError(
                "[InspectPanelController] InspectImage가 연결되지 않았습니다."
            );
        }


        // 시작 시 패널 닫기
        if (inspectPanel != null)
        {
            inspectPanel.SetActive(false);
        }
    }


    // =====================================================
    // 패널 열기
    // =====================================================
    public void OpenPanel(Sprite spriteToShow)
    {
        Debug.LogWarning(
            "[InspectPanelController] OpenPanel 호출됨"
        );


        if (inspectPanel == null)
        {
            Debug.LogError(
                "[InspectPanelController] InspectPanel이 NULL입니다."
            );

            return;
        }


        if (inspectImage == null)
        {
            Debug.LogError(
                "[InspectPanelController] InspectImage가 NULL입니다."
            );

            return;
        }


        if (spriteToShow == null)
        {
            Debug.LogError(
                "[InspectPanelController] 표시할 Sprite가 NULL입니다."
            );

            return;
        }


        // 이미지 지정
        inspectImage.sprite = spriteToShow;


        // 패널 켜기
        inspectPanel.SetActive(true);


        // 같은 Canvas에서 가장 앞쪽에 보이도록 이동
        inspectPanel.transform.SetAsLastSibling();


        Debug.LogWarning(
            "[InspectPanelController] InspectPanel 활성화 완료"
        );
    }


    // =====================================================
    // 패널 닫기
    // =====================================================
    public void ClosePanel()
    {
        if (inspectPanel != null)
        {
            inspectPanel.SetActive(false);
        }


        Debug.LogWarning(
            "[InspectPanelController] InspectPanel 닫기"
        );
    }
}
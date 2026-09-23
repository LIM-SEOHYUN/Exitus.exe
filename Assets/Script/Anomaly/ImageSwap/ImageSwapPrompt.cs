using UnityEngine;

public class ImageSwapPrompt : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("E Prompt")]
    public GameObject promptObject;

    [Header("Interactable Entity")]
    public InteractableEntity interactableEntity;

    [Header("Distance")]
    public float showDistance = 2.5f;

    [Header("Debug Test")]
    public bool forcePromptForTest = false;


    private void Awake()
    {
        // 같은 오브젝트의 InteractableEntity 자동 검색
        if (interactableEntity == null)
        {
            interactableEntity =
                GetComponent<InteractableEntity>();
        }


        // Player가 Inspector에서 연결되지 않았다면
        // PlayerInteraction을 통해 자동 검색
        if (player == null)
        {
            PlayerInteraction playerInteraction =
                FindObjectOfType<PlayerInteraction>();

            if (playerInteraction != null)
            {
                player =
                    playerInteraction.transform;
            }
        }
    }


    private void Start()
    {
        HidePromptImmediately();
    }


    private void Update()
    {
        if (promptObject == null)
        {
            return;
        }


        if (player == null)
        {
            promptObject.SetActive(false);
            return;
        }


        if (interactableEntity == null)
        {
            promptObject.SetActive(false);
            return;
        }


        // =================================================
        // [E] 표시 허용 여부
        // =================================================

        // 실제 ImageSwap 이상현상이 발동됐을 때만 true
        bool actualAnomalyActive =
            interactableEntity.ShouldShowPrompt();


        // 테스트할 때만 강제로 표시 허용
        bool promptAllowed =
            actualAnomalyActive || forcePromptForTest;


        if (!promptAllowed)
        {
            promptObject.SetActive(false);
            return;
        }


        // =================================================
        // Player와 그림 사이 거리
        // =================================================

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );


        if (distance <= showDistance)
        {
            promptObject.SetActive(true);
        }
        else
        {
            promptObject.SetActive(false);
        }
    }


    // 다른 스크립트에서도 즉시 숨길 수 있도록 public
    public void HidePromptImmediately()
    {
        if (promptObject != null)
        {
            promptObject.SetActive(false);
        }
    }


    // Scene에서 상호작용 거리 확인
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            showDistance
        );
    }
}
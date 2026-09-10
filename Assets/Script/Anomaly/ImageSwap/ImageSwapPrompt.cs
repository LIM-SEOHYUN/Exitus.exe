using UnityEngine;

public class ImageSwapPrompt : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("E Prompt")]
    public GameObject promptObject;

    [Header("Distance")]
    public float showDistance = 1.5f;

    // 이상현상이 현재 활성화되어 있는가?
    private bool anomalyActive = false;

    private void Start()
    {
        if (promptObject != null)
        {
            promptObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null || promptObject == null)
            return;

        // 이상현상이 발동하지 않았다면 무조건 숨김
        if (!anomalyActive)
        {
            promptObject.SetActive(false);
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // 이상현상 활성화 + 가까이 있을 때만 E 표시
        if (distance <= showDistance)
        {
            promptObject.SetActive(true);
        }
        else
        {
            promptObject.SetActive(false);
        }
    }

    public void EnablePrompt()
    {
        anomalyActive = true;
    }

    public void DisablePrompt()
    {
        anomalyActive = false;

        if (promptObject != null)
        {
            promptObject.SetActive(false);
        }
    }
}
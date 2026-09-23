using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionRange = 2.5f;

    public KeyCode interactionKey = KeyCode.E;

    private InteractableEntity nearestEntity;


    private void Update()
    {
        FindNearestEntity();


        // E키를 누름
        if (Input.GetKeyDown(interactionKey))
        {
            if (nearestEntity != null)
            {
                nearestEntity.Interact();
            }
            else
            {
                Debug.Log(
                    "[PlayerInteraction] 근처에 상호작용 가능한 대상이 없습니다."
                );
            }
        }
    }


    // =====================================================
    // 가장 가까운 상호작용 대상을 찾음
    // =====================================================
    private void FindNearestEntity()
    {
        InteractableEntity[] entities =
            FindObjectsOfType<InteractableEntity>();

        nearestEntity = null;

        float nearestDistance = interactionRange;


        foreach (InteractableEntity entity in entities)
        {
            // 현재 E키로 상호작용할 수 없는 대상이면 무시
            if (!entity.CanInteract())
            {
                continue;
            }


            float distance = Vector2.Distance(
                transform.position,
                entity.transform.position
            );


            if (distance <= nearestDistance)
            {
                nearestDistance = distance;
                nearestEntity = entity;
            }
        }
    }


    // Player를 Scene에서 선택하면
    // E키 상호작용 범위를 확인할 수 있게 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            interactionRange
        );
    }
}
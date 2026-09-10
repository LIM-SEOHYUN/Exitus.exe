using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionRange = 1.5f;
    public KeyCode interactionKey = KeyCode.E;

    private InteractableEntity nearestEntity;

    private void Update()
    {
        FindNearestEntity();

        if (Input.GetKeyDown(interactionKey))
        {
            if (nearestEntity != null)
            {
                nearestEntity.Interact();
            }
            else
            {
                Debug.Log("[PlayerInteraction] 상호작용 가능한 대상이 근처에 없습니다.");
            }
        }
    }

    private void FindNearestEntity()
    {
        InteractableEntity[] entities = FindObjectsOfType<InteractableEntity>();

        nearestEntity = null;
        float nearestDistance = interactionRange;

        foreach (InteractableEntity entity in entities)
        {
            float distance = Vector2.Distance(transform.position, entity.transform.position);

            if (distance <= nearestDistance)
            {
                nearestDistance = distance;
                nearestEntity = entity;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
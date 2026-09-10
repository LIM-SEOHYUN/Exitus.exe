using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;          // 플레이어 Transform
    public float openDistance = 0.1f;   // 열리는 거리
    public Animator animator;
    private Collider2D doorCollider;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        isOpen = false;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (!isOpen && distance <= openDistance)
        {
            OpenDoor();
        }
        else if (isOpen && distance > openDistance + 1f) // 플레이어가 멀어지면 닫힘
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        animator.SetTrigger("Open");
        doorCollider.enabled = false; // 통과 가능
    }

    void CloseDoor()
    {
        isOpen = false;
        animator.SetTrigger("Close");
        doorCollider.enabled = true; // 다시 막힘
    }
}

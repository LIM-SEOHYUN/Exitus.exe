using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;
    public float openDistance = 0.1f;
    public Animator animator;

    [Header("Door Sound Override")]
    [Tooltip("비워두면 부모 door의 DoorSoundBank를 사용합니다.")]
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;
    public float doorSoundVolume = 1f;

    private Collider2D doorCollider;
    private AudioSource localAudioSource;
    private DoorSoundBank parentSoundBank;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        parentSoundBank = GetComponentInParent<DoorSoundBank>();

        isOpen = false;
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (!isOpen && distance <= openDistance)
        {
            OpenDoor();
        }
        else if (isOpen && distance > openDistance + 1f)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;

        if (animator != null)
            animator.SetTrigger("Open");

        if (doorCollider != null)
            doorCollider.enabled = false;

        Debug.Log("[DoorController] " + gameObject.name + " 문 열림");

        if (doorOpenClip != null)
        {
            PlayLocalSound(doorOpenClip);
        }
        else if (parentSoundBank != null)
        {
            parentSoundBank.PlayOpen();
        }
        else
        {
            Debug.LogWarning(
                "[DoorController] " + gameObject.name +
                ": 부모 DoorSoundBank를 찾지 못했습니다."
            );
        }
    }

    void CloseDoor()
    {
        isOpen = false;

        if (animator != null)
            animator.SetTrigger("Close");

        if (doorCollider != null)
            doorCollider.enabled = true;

        if (doorCloseClip != null)
        {
            PlayLocalSound(doorCloseClip);
        }
        else if (parentSoundBank != null)
        {
            parentSoundBank.PlayClose();
        }
    }

    void PlayLocalSound(AudioClip clip)
    {
        if (localAudioSource == null)
        {
            localAudioSource = gameObject.AddComponent<AudioSource>();
            localAudioSource.playOnAwake = false;
            localAudioSource.loop = false;
            localAudioSource.spatialBlend = 0f;
        }

        localAudioSource.PlayOneShot(clip, doorSoundVolume);
    }
}

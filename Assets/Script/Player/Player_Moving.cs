using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;

    private Vector2 movement;

    [Header("Footstep")]
    public AudioClip[] footstepClips;
    public float footstepInterval = 0.35f;

    private float footstepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        footstepTimer = 0f;
    }

    void Update()
    {
        if (!GameManager.Instance.canMove) // 화면 암전시 이동 제한
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }

        Movement();
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        bool isWalking = movement.sqrMagnitude > 0;
        animator.SetBool("isWalking", isWalking);

        if (moveX < 0)
            spriteRenderer.flipX = true;
        else if (moveX > 0)
            spriteRenderer.flipX = false;

        // Play footsteps while the player is moving.
        if (isWalking)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            // Play a footstep immediately when movement starts again.
            footstepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (audioSource == null || footstepClips == null || footstepClips.Length == 0)
            return;

        int randomIndex = Random.Range(0, footstepClips.Length);
        AudioClip clip = footstepClips[randomIndex];

        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}

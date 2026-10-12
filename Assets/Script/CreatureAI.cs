using UnityEngine;
using System.Collections;

public class CreatureAI : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 3f;
    public Transform player;
    public float fadeDuration = 1f;

    public AudioClip alertClip;
    [Range(0f, 1f)] public float alertVolume = 0.8f;

    private bool isActive = false;
    private bool isAttacking = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D myCollider;
    private AudioSource alertAudioSource;

    void Awake()
    {
        alertAudioSource = gameObject.AddComponent<AudioSource>();
        alertAudioSource.playOnAwake = false;
        alertAudioSource.loop = false;
        alertAudioSource.spatialBlend = 0f;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;

        // 기본적으로 충돌 꺼두기
        if (myCollider != null)
            myCollider.enabled = false;
    }

    void Update()
    {
        if (isActive && !isAttacking)
        {
            ChasePlayer();
        }
    }

    // 외부(AnomalyManager → MonsterChaseAnomaly)에서 호출
    public void Activate()
    {
        isActive = true;
        if (alertClip != null)
            alertAudioSource.PlayOneShot(alertClip, alertVolume);
        if (myCollider != null)
            myCollider.enabled = true; // 활성화될 때만 충돌 켜기
        StartCoroutine(FadeIn());
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        if (!isAttacking) // 공격 중에는 걷기 애니메이션/플립 막기
        {
            animator.SetBool("isWalking", true);

            // x 방향이 충분히 클 때만 플립 (임계값 0.1)
            if (direction.x > 0.1f)
                spriteRenderer.flipX = false;
            else if (direction.x < -0.1f)
                spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 감지: " + other.name);
        if (!isActive) return; // 활성화되지 않았으면 무시

        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            if (animator != null)
                animator.SetTrigger("Attack");

            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        moveSpeed = 0f;
        if (animator != null) animator.SetBool("isWalking", false);

        if (CutsceneManager.Instance != null)
        {
            yield return StartCoroutine(CutsceneManager.Instance.PlayCutsceneRoutine());
        }
        else
        {
            Debug.LogError("씬에 CutsceneManager가 없습니다! 빈 오브젝트를 만들고 매니저 스크립트를 넣어주세요.");
        }

        GameManager.Instance.Fail();

        // Destroy(gameObject) 대신 숨기기 실행
        DeactivateMonster();
    }

    public void DeactivateMonster()
    {
        isActive = false;
        isAttacking = false;

        if (animator != null) animator.SetBool("isWalking", false);
        if (myCollider != null) myCollider.enabled = false;

        // 파괴하지 않고 숨김 처리 (재사용 가능)
        gameObject.SetActive(false);
    }
}
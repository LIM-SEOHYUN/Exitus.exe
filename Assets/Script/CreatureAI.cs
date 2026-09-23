using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class CreatureAI : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 3f;
    public Transform player;
    public float fadeDuration = 1f;

    public GameObject cutsceneUI; // UI 패널
    public VideoPlayer videoPlayer; // VideoPlayer 연결

    private bool isActive = false;
    private bool isAttacking = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D myCollider;

    void Awake()
    {
        if (cutsceneUI == null)
            cutsceneUI = GameObject.Find("CutsceneUI");

        if (videoPlayer == null)
            videoPlayer = FindObjectOfType<VideoPlayer>();
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

        if (cutsceneUI != null)
            cutsceneUI.SetActive(false);

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
        if (cutsceneUI != null)
            cutsceneUI.SetActive(true);

        if (videoPlayer != null)
        {
            Debug.Log("영상 재생 시작");
            videoPlayer.Play();
            // 영상 길이만큼 대기
            yield return new WaitForSeconds((float)videoPlayer.length);
            videoPlayer.Stop();
        }

        if (cutsceneUI != null)
            cutsceneUI.SetActive(false);

        moveSpeed = 0f;
        animator.SetBool("isWalking", false);

        GameManager.Instance.Fail();
        Destroy(gameObject);
    }
}

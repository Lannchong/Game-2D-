using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Health & Game Over Settings")]
    public int health = 100;
    private int maxHealth;
    public Image healthBar;
    public float fallThreshold = -10f;
    public GameObject gameOverPanel;
    public GameObject uiSkor; 

    [Header("Score Settings")]
    public int coinScore = 0;
    public TMP_Text scoreText;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip coinSound;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float horizontalInput;
    private bool isGrounded;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        maxHealth = health;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateHealthBar();
    }

    void Update()
    {
        if (isDead) return;

        // CEK JATUH KE JURANG
        if (transform.position.y < fallThreshold)
        {
            health = 0;
            UpdateHealthBar();
            Die();
        }

        // CEK TANAH
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        if (Keyboard.current == null) return;

        // MENGATUR ARAH GERAK
        horizontalInput = 0f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
            spriteRenderer.flipX = false;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
            spriteRenderer.flipX = true;
        }

        // FITUR LOMPAT + SUARA LOMPAT
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Putar suara lompat
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        SetAnimation(horizontalInput);
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0) animator.Play("Player_Idle");
            else animator.Play("Player_Run");
        }
        else
        {
            if (rb.linearVelocity.y > 0) animator.Play("Player_Jump");
            else animator.Play("Player_Fall");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        // FITUR KENA DAMAGE + SUARA DAMAGE
        if (collision.gameObject.tag == "Damage")
        {
            health -= 25;
            UpdateHealthBar();

            // Putar suara damage
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        // --- KODE MEMATIKAN BGM SAAT GAME OVER ---
        GameObject bgm = GameObject.Find("BackgroundMusic");
        if (bgm != null)
        {
            bgm.GetComponent<AudioSource>().Stop(); // Matikan lagu BGM
        }
        // -----------------------------------------

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // --- BARU: KODE MEMATIKAN UI SKOR DAN DARAH SAAT MATI ---
        if (uiSkor != null)
        {
            uiSkor.SetActive(false);
        }
        // --------------------------------------------------------

        // Hentikan waktu game (pause) agar musuh/animasi latar ikut berhenti
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // Kembalikan waktu normal sebelum me-load ulang scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // FITUR AMBIL KOIN + SUARA KOIN
    public void AddCoin(int amount)
    {
        coinScore += amount;
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + coinScore.ToString();
        }

        // Putar suara koin
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }
}
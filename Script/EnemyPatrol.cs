using UnityEngine;

// Memastikan objek ini punya Rigidbody2D dan Collider2D secara otomatis
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points (Tarik dari Hierarchy)")]
    public Transform point1;
    public Transform point2;

    [Header("Movement Settings")]
    public float speed = 2f;

    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        // Melepaskan Point1 dan Point2 dari Parent (EnemyGroup) 
        // agar tidak ikut bergerak saat musuh berjalan. SANGAT PENTING!
        point1.parent = null;
        point2.parent = null;

        // Mendapatkan komponen-komponen yang diperlukan
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Musuh mulai berjalan menuju Point 2
        currentTarget = point2;
    }

    void Update()
    {
        // Menghitung arah horizontal ke target (hanya X, abaikan Y)
        float directionX = currentTarget.position.x - transform.position.x;
        Vector2 movementDirection = new Vector2(directionX, 0).normalized;

        // Menggerakkan musuh menggunakan KECEPATAN FISIK (bukan mengubah posisi langsung)
        // Ini akan membuat musuh mematuhi Collider tanah dan tidak menembusnya.
        rb.linearVelocity = new Vector2(movementDirection.x * speed, rb.linearVelocity.y);

        // Mengatur flip gambar (Sprite)
        // Mengatur flip gambar (Sprite)
        if (directionX > 0) // Jika musuh bergerak ke Kanan
        {
            spriteRenderer.flipX = true; // Balik gambar 
        }
        else if (directionX < 0) // Jika musuh bergerak ke Kiri
        {
            spriteRenderer.flipX = false; // Kembalikan ke gambar aslinya
        }
        // Mengecek apakah sudah sampai di titik target
        float distance = Vector2.Distance(transform.position, currentTarget.position);
        if (distance < 0.2f)
        {
            // Tukar target
            if (currentTarget == point1)
            {
                currentTarget = point2;
            }
            else
            {
                currentTarget = point1;
            }
        }
    }
}
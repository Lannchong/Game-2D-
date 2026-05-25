using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh koin adalah objek dengan Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mengambil script PlayerMovement dari si Player
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            // Jika scriptnya ketemu, jalankan fungsi nambah koin
            if (player != null)
            {
                player.AddCoin(1); // Tambah 1 poin

                // Hancurkan/hilangkan koin dari game
                Destroy(gameObject);
            }
        }
    }
}
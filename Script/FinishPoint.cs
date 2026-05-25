using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject winPanel;

    [Header("Save System")]
    [Tooltip("Level berapa yang akan terbuka setelah menyentuh bendera ini? (Contoh: isi 2 jika ini bendera di Level 1)")]
    public int unlockLevelNumber;

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }

            // --- KODE SAVE SYSTEM ---
            // Cek level berapa yang sudah terbuka saat ini (default-nya 1)
            int currentUnlockedLevel = PlayerPrefs.GetInt("LevelTerbuka", 1);

            // Jika level yang mau dibuka (unlockLevelNumber) lebih tinggi dari rekor saat ini, simpan data baru!
            if (unlockLevelNumber > currentUnlockedLevel)
            {
                PlayerPrefs.SetInt("LevelTerbuka", unlockLevelNumber);
                PlayerPrefs.Save(); // Pastikan data tersimpan
            }
            // ------------------------

            // --- KODE MEMATIKAN BGM SAAT MENANG ---
            // Mencari objek bernama "BackgroundMusic" di dalam game
            GameObject bgm = GameObject.Find("BackgroundMusic");
            if (bgm != null)
            {
                bgm.GetComponent<AudioSource>().Stop(); // Matikan lagunya
            }
            // --------------------------------------

            Time.timeScale = 0f;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
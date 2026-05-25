using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject pauseMenuPanel;

    public static bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // --- KODE MENJEDA BGM SAAT PAUSE ---
        GameObject bgm = GameObject.Find("BackgroundMusic");
        if (bgm != null)
        {
            // Menggunakan Pause() agar lagu berhenti sementara di detik tersebut
            bgm.GetComponent<AudioSource>().Pause();
        }
        // -----------------------------------
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // --- KODE MELANJUTKAN BGM SAAT RESUME ---
        GameObject bgm = GameObject.Find("BackgroundMusic");
        if (bgm != null)
        {
            // Menggunakan UnPause() agar lagu lanjut dari detik terakhir
            bgm.GetComponent<AudioSource>().UnPause();
        }
        // ----------------------------------------
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
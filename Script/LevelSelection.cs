using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("Urutan Tombol: Level 1, Level 2, Level 3")]
    public Button[] levelButtons;

    void Start()
    {
        UpdateLevelButtons();
    }

    public void UpdateLevelButtons()
    {
        int levelTerbuka = PlayerPrefs.GetInt("LevelTerbuka", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelTerbuka)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    // --- TAMBAHKAN FUNGSI INI ---
    public void ResetProgress()
    {
        // Menghapus data "LevelTerbuka" dari memori perangkat
        PlayerPrefs.DeleteKey("LevelTerbuka");

        // Memperbarui tampilan tombol agar langsung terkunci kembali saat itu juga
        UpdateLevelButtons();

        Debug.Log("Data berhasil di-reset! Semua level kembali terkunci.");
    }
}
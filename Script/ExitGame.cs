using UnityEngine;
using System.Collections;
// Jika menggunakan Unity Editor, tambahkan pustaka ini untuk menangani mode Play
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGame : MonoBehaviour
{
    // Fungsi publik ini dapat dipanggil dari tombol UI Unity (Button component -> OnClick())
    public void Quit()
    {
        // Catat ke konsol sebagai konfirmasi bahwa tombol ditekan
        Debug.Log("Tombol Keluar Ditekan. Memulai proses keluar...");

        // Keluar dari aplikasi yang sudah dibangun (build aplikasi)
        Application.Quit();

        // Tangani khusus untuk Unity Editor agar mode Play berhenti
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class PindahScene : MonoBehaviour
{
    // Fungsi ini sangat sakti karena bisa menerima nama Scene apa saja dari Inspector!
    public void KeLevel(string namaScene)
    {
        // Mengembalikan waktu berjalan normal (jaga-jaga kalau sebelumnya dari Pause/Game Over)
        Time.timeScale = 1f;

        SceneManager.LoadScene(namaScene);
    }
}
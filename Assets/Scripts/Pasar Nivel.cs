using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    // El método correcto para Triggers (Gatillos)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

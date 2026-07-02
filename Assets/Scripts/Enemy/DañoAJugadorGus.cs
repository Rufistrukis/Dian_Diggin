using UnityEngine;

public class DañoAJugadorGus : MonoBehaviour
{
    [SerializeField] private int dañoPorToque;

    private void Collider2D (Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(dañoPorToque);
        }
    }
}


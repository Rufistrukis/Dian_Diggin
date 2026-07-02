using UnityEngine;

public class DañoAJugador : MonoBehaviour
{
    [SerializeField] private int dañoPorToque;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(dañoPorToque);
            Vector2 direcciondanio = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().RecibeDanio(direcciondanio, 1);
        }
    }
}

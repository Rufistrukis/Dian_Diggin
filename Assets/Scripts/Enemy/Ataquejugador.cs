using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{

    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            IntentarAtacar();
        }
    }

    private void IntentarAtacar()
    {
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) { return; }
            Atacar();
    }

    private void Atacar()

    {
        tiempoUltimoAtaque = Time.time; //tiempo que el juego lleva en ejecución

        Collider2D[] objetosTocados = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.TryGetComponent(out VidaEnemigo vidaEnemigo)) //toma el componente de un objeto
            {
                vidaEnemigo.TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}

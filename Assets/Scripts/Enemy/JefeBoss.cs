using UnityEngine;

public class JefeBoss : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public Rigidbody2D rb2D;
    public Animator animator;

    [Header("Salto aleatorio")]
    public float fuerzaSalto = 6f;
    public float tiempoMinEntreSaltos = 2f;
    public float tiempoMaxEntreSaltos = 5f;
    public Transform puntoChequeoSuelo;
    public float radioChequeoSuelo = 0.2f;
    public LayerMask capaSuelo;

    private float temporizadorSalto;
    private bool enElSuelo;

    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
    }

    void Start()
    {
        puntoInicial = transform.position;
        ReiniciarTemporizadorSalto();
    }

    void Update()
    {
        enElSuelo = Physics2D.OverlapCircle(puntoChequeoSuelo.position, radioChequeoSuelo, capaSuelo);

        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;
        }

        ManejarSaltoAleatorio();
    }

    private void ManejarSaltoAleatorio()
    {
        // Si quieres que SOLO salte mientras sigue o vuelve, agrega aquí la condición de estado.
        temporizadorSalto -= Time.deltaTime;

        if (temporizadorSalto <= 0f && enElSuelo)
        {
            Saltar();
            ReiniciarTemporizadorSalto();
        }
    }

    private void Saltar()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, fuerzaSalto);
        // animator.SetTrigger("Saltar"); // si tienes animación de salto
    }

    private void ReiniciarTemporizadorSalto()
    {
        temporizadorSalto = Random.Range(tiempoMinEntreSaltos, tiempoMaxEntreSaltos);
    }

    private void EstadoEsperando()
    {
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);
        if (jugadorCollider)
        {
            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        if (transform.position.x < transformJugador.position.x)
        {
            rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(-velocidadMovimiento, rb2D.linearVelocity.y);
        }

        GirarAObjetivo(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima || Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    private void EstadoVolviendo()
    {
        if (transform.position.x < puntoInicial.x)
        {
            rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(-velocidadMovimiento, rb2D.linearVelocity.y);
        }

        GirarAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 0.1f)
        {
            rb2D.linearVelocity = Vector2.zero;
            animator.SetBool("Corriendo", false);
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void GirarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);

        if (puntoChequeoSuelo != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoChequeoSuelo.position, radioChequeoSuelo);
        }
    }
}
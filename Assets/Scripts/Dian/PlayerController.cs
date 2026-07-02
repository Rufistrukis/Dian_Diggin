using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 6f;
    public float fuerzaRebote = 10f;

    public float longitudRaycast = 0.2f;
    public LayerMask capaSuelo;

    [Header("Doble Salto")]
    public bool puedeDobleSpermitido = false;

    private bool puedeDobleS = false;
    private bool enSuelo;
    
    private bool Atacando;
    private bool down;
    private bool Excavando;
    public bool recibiendoodanio;
  
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        if (!Atacando)
        {
            // MOVIMIENTO HORIZONTAL (FÍSICAS)
            float velocidadX = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(velocidadX * velocidad, rb.linearVelocity.y);

            animator.SetFloat("ovement", Mathf.Abs(velocidadX));

            if (velocidadX > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (velocidadX < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            // RAYCAST SUELO
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                longitudRaycast,
                capaSuelo
            );

            enSuelo = hit.collider != null;

            // RESET DOBLE SALTO
            if (enSuelo)
            {
                puedeDobleS = true;
            }

            // SALTO + DOBLE SALTO
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (enSuelo)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                }
                else if (puedeDobleSpermitido && puedeDobleS)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                    puedeDobleS = false;
                }
            }

            // ACCIONES EXTRA
            if (Input.GetKeyDown(KeyCode.DownArrow) && !down)
                downing();

            if (Input.GetKeyDown(KeyCode.E) && !Excavando)
                Excavandoo();
        }

        // ANIMACIONES
        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("atacando", Atacando);
        animator.SetBool("recoger", down);
        animator.SetBool("excava", Excavando);
        animator.SetBool ("recibedanio", recibiendoodanio);

        if (Input.GetKeyDown(KeyCode.X) && !Atacando)
            atacando();
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        recibiendoodanio = true;
        Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
        rb.AddForce(rebote*fuerzaRebote, ForceMode2D. Impulse);
    }

    public void Desactivadanio()
    {
        recibiendoodanio = false;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }



    public void atacando()
    {
        Atacando = true;
    }

    public void NoAtacando()
    {
        Atacando = false;
    }

    public void downing()
    {
        down = true;
    }

    public void NoDowning()
    {
        down = false;
    }

    public void Excavandoo()
    {
        Excavando = true;
    }

    public void NoExcavandoo()
    {
        Excavando = false;
    }
}

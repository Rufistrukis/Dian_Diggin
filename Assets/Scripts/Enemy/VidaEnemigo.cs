using JetBrains.Annotations;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{

    private Animator animator;
    private bool damage;
    private bool muelte;
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int cantidadDeDaño)
    {
        damage = true;
        int cantidadDeVidaTemporal = vidaActual - cantidadDeDaño;

        cantidadDeVidaTemporal = Mathf.Clamp(cantidadDeVidaTemporal, 0, vidaMaxima); // hace la función de un if, toma el valor del primer parametro y le pone el rango de los siguientes dos (2do el minim., 3ro el max.)

        vidaActual = cantidadDeVidaTemporal;

        if (vidaActual == 0 )
        {
            Destroy(gameObject);
            muelte = true;
        }

        animator.SetBool("dam", damage);
        animator.SetBool("MUELTO", muelte);
    }

        public void muere()
        {
            muelte = true;
        }

        public void daño()
    {
        damage = true;
    }
    public void desactivadaño()
    {
        damage = false;
    }

    }


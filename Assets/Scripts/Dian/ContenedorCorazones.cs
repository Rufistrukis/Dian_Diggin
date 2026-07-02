using System;
using UnityEngine;

public class ContenedorCorazones : MonoBehaviour
{
    [SerializeField] private CorazonUI[] corazones;
    [SerializeField] private VidaJugador vidaJugador;

    private void Start()
    {
        // Si no lo arrastraste en el Inspector, búscalo automáticamente
        if (vidaJugador == null)
        {
            vidaJugador = FindFirstObjectByType<VidaJugador>();
        }

        if (vidaJugador != null)
        {
            // Receptores de señal (Suscribirse)
            vidaJugador.JugadorTomoDaño += ActivarCorazones; 
            vidaJugador.JugadorSeCuro += ActivarCorazones; 

            // Inicializa los corazones con la vida que tiene el jugador al empezar
            ActivarCorazones(vidaJugador.GetVidaActual());
        }
        else
        {
            Debug.LogError("¡No se encontró el script VidaJugador en la escena!");
        }
    }

    private void OnDisable()
    {
        // CORRECCIÓN: Validamos que el jugador todavía exista antes de desuscribirnos 
        // para evitar errores de consola al cambiar de escena o al morir.
        if (vidaJugador != null)
        {
            vidaJugador.JugadorTomoDaño -= ActivarCorazones; 
            vidaJugador.JugadorSeCuro -= ActivarCorazones; 
        }
    }

    private void ActivarCorazones(int vidaActual)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
            {

                if(corazones[i].EstaActivo()){continue;}
                corazones[i].ActivarCorazon();
            }
            else
            {
                if(!corazones[i].EstaActivo()){continue;}
                corazones[i].DesactivarCorazon();
            }
        }
    }
}
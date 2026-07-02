using System;
using System.Numerics;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public Action<int> JugadorTomoDaño; //señales que algun proceso escuchara
    public Action<int> JugadorSeCuro; //señales que algun proceso escuchara

    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;
    private bool recibiendoDanio;
    private bool die;

    

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño( int daño)
    {
        recibiendoDanio = true;
       
        int vidaTemporal = vidaActual - daño;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);

        vidaActual = vidaTemporal;

        JugadorTomoDaño?.Invoke(vidaActual); //enviara una señal a cualquier proceso que lo necesite, ? es por si nadie lo recibe nadie lo reciba

        if (vidaActual <= 0)
        {
            DestruirJugador();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void DestruirJugador()
    {
        Destroy(gameObject);
    }

    public void CurarVida(int curacion)
    {
        int vidaTemporal = vidaActual + curacion;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);

        vidaActual = vidaTemporal;

        JugadorSeCuro?.Invoke(vidaActual);
    }

    public int GetVidaMaxima() => vidaMaxima;
    public int GetVidaActual() => vidaActual;
}

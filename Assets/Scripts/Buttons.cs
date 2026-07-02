using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void salir()
    {
         
        Debug.Log("Salir del juego");
        Application.Quit();
       
    }

    public void ircredits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void irmenu()
    {
        SceneManager.LoadScene("Inicio");
    }
}
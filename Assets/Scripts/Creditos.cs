using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("VolverMenu", 97.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VolverMenu();
        }
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void iracredits()
    {
        SceneManager.LoadScene("Creditos");
    }
}

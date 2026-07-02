using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public GameObject gameOverPanel;
  public TextMeshProUGUI gameOverText;
  public Button reiniciarButton;
  public Button menuButton;


  private bool gameOverActivo = false;

  
  void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           ;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  void Start()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if(reiniciarButton != null)
        {
            reiniciarButton.onClick.AddListener(ReiniciarEscen);
        }

        if(menuButton != null)
        {
            menuButton.onClick.AddListener(IrMenu);
        }
        
    }


    void Update()
    {
        if (gameOverActivo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReiniciarEscen();
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
            {
                IrMenu();
            }
        }
    }

    public void GameOver()
    {
        if(gameOverActivo)
        {
            return;
        }
        gameOverActivo = true;
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if(gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
        }
    }

    public void ReiniciarEscen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Inicio");
    }
}

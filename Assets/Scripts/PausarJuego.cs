using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        if (menuPausa != null) menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    public void Pausar()
    {
        if (menuPausa != null) menuPausa.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    public void irAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

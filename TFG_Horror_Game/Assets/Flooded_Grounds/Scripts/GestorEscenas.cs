using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenas : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Scene_A");
    }

    public void Credits()
    {
        SceneManager.LoadScene("DevsMenu");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

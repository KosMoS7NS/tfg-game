using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
        Environment.Exit(0);
    }
}

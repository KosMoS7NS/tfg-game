using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject camara2;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image life1;
    [SerializeField] private Image life2;
    [SerializeField] private Image life3;
    [SerializeField] private GameObject blood;
    [SerializeField] private Image gameOver;
    [SerializeField] private Text notes;

    // Funci�n para borrar el texto del canvas y resetear color
    public void DeleteText()
    {
        setPermanentText("");
        setColorText(Color.grey);
    }

    // Funci�n para a�adir un texto al canvas de forma temporal
    public void setText(string newText)
    {
        CancelInvoke("DeleteText");
        text.text = newText;
        Invoke("DeleteText", 5);
    }

    // Funci�n para a�adir un texto al canvas de forma permanente (hasta que se indique)
    public void setPermanentText(string newText)
    {
        CancelInvoke("DeleteText");
        text.text = newText;
    }

    // Funci�n para definir el color del texto
    public void setColorText(Color color)
    {
        text.color = color;
    }

    // Funci�n para que el jugador reciba da�o
    public void Damage()
    {
        BloodSplash();
        if (life1.isActiveAndEnabled)
            life1.gameObject.SetActive(false);
        else if (life2.isActiveAndEnabled)
            life2.gameObject.SetActive(false);
        else if (life3.isActiveAndEnabled)
            life3.gameObject.SetActive(false);
        else
        {
            GameOver();
        }
    }

    // Funci�n para que el jugador se cure
    public void Heal()
    {
        if (!life2.isActiveAndEnabled)
            life2.gameObject.SetActive(true);
        else if (!life3.isActiveAndEnabled)
            life3.gameObject.SetActive(true);
    }

    // Funci�n para que el jugador se cure totalmente
    public void DivineHeal()
    {
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);
    }

    // Funci�n para activar salpicaduras de sangre en la pantalla cuando recibimos da�o
    private void BloodSplash()
    {
        blood.SetActive(true);
        Invoke("DesactiveBlood", 3);
    }
    private void DesactiveBlood()
    {
        blood.SetActive(false);
    }

    // Pensamientos de la persona (Notes)
    public void Notes() {
        notes.gameObject.SetActive(false);
    }

    // Funci�n para terminar la partida si el jugador muere
    private void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        player.SetActive(false);
        camara2.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Notes", 5.0f, 0.0f);
        DeleteText();
        DivineHeal();
        setText("W/A/S/D or Arrow Keys to move");
    }    
}

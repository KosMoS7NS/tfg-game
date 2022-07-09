using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Sprite VolumeButtonImg;
    [SerializeField] private Sprite MuteButtonImg;
    [SerializeField] private Button MuteButton;
    [SerializeField] private Slider audioSlider;

    private CanvasController canvasController;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Función para activar o desactivar el mute a través del botón
    public void MuteButtonOnClickListener()
    {
        if (MuteButton.transform.GetChild(0).GetComponent<Image>().sprite == VolumeButtonImg)
            audioSlider.value = 0;
        else
            audioSlider.value = 0.1f;
        UpdateVolume();
    }

    // Función para controlar el volumen del juego
    public void UpdateVolume()
    {
        if (audioSlider.value == 0)
        {
            MuteButton.transform.GetChild(0).GetComponent<Image>().sprite = MuteButtonImg;
            canvasController.player.transform.GetChild(0).GetComponent<AudioListener>().enabled = false;
        }

        else
        {
            MuteButton.transform.GetChild(0).GetComponent<Image>().sprite = VolumeButtonImg;
            canvasController.player.transform.GetChild(0).GetComponent<AudioListener>().enabled = true;
        }

        Debug.Log("Max: " + audioSlider.maxValue + "\nMin: " + audioSlider.minValue + "\nNow: " + audioSlider.value);
    }

    public void Save()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        canvasController = FindObjectOfType<CanvasController>();
        audioSlider.value = 0.5f;
    }
}

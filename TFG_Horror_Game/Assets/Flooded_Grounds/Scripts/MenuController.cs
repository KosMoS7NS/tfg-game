using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;
    private AudioSource audio;
    
    public void VolumeUpdate()
    {
        audio.volume = audioSlider.value;
    }
}

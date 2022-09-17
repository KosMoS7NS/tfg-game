using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPick : MonoBehaviour
{
    [SerializeField] private AudioClip m_TakeObjSound;
    public Inventory inventory;
    public GameObject linterna;
    public GameObject bateriesPanel;
    private BateryController bateryController;
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        bateryController = FindObjectOfType<BateryController>();
        linterna.SetActive(false);
        this.gameObject.SetActive(true);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        bateriesPanel.SetActive(false);
    }

    private void RemoveEnergy()
    {
        bateryController.RemoveBatery();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            bateryController.AddBatery();
            linterna.SetActive(true);
            inventory.cantidad += 1;
            this.gameObject.SetActive(false);
            bateriesPanel.SetActive(true);
            m_AudioSource.clip = m_TakeObjSound;
            m_AudioSource.Play();
            InvokeRepeating("RemoveEnergy", 5, 5);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPick : MonoBehaviour
{
    public Inventory inventory;
    public GameObject linterna;
   // Vector3 vector = new Vector3(0.15, -0.35, 0.6);

    void Start()
    {
        linterna.SetActive(false);
        this.gameObject.SetActive(true);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            linterna.SetActive(true);
            // var postion = gameObject.GetComponent<Transform>().position;
            // postion = new Vector3(0, 4, 8);
            inventory.cantidad += 1;
            this.gameObject.SetActive(false);
        }
    }
}

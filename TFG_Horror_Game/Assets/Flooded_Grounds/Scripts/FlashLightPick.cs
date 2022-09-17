using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPick : MonoBehaviour
{
    public Inventory inventory;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            inventory.cantidad += 1;
            Destroy(gameObject);
        }
    }
}

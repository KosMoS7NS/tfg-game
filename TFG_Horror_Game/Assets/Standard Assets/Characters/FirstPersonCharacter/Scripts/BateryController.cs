using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateryController : MonoBehaviour
{
    [SerializeField] private GameObject batery;         // Prefab de la pila
    [SerializeField] private GameObject bateriesPanel;
    [SerializeField] private GameObject torchLight;     // Permitir� apagar la linterna cuando se quede sin bater�a
    [SerializeField] private GameObject life;           // Se usar� solamente para tener en cuenta la posici�n y posicionar las pilas
    public bool energy;                                 // Permitir� saber desde otros scripts (player) si hay bater�a en la linterna
    private Vector3 position;                           // Vector para posicionar las pilas (justo debajo de las vidas)

    private void Start()
    {
        position = life.transform.position;
        position.x += 35;
        position.y -= 50;
        energy = false;
    }

    public void AddBatery()
    {
        energy = true;
        position.x -= 25;
        GameObject newBatery = Instantiate(batery, position, batery.transform.rotation);
        newBatery.transform.SetParent(bateriesPanel.transform);
    }

    public void RemoveBatery()
    {

        if (bateriesPanel.transform.childCount > 0)
            Destroy(bateriesPanel.transform.GetChild(bateriesPanel.transform.childCount - 1).gameObject);
        else
        {
            torchLight.SetActive(false);
            energy = false;
        }
    }
}

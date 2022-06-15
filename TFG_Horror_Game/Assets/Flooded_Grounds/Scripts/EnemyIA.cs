using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour

{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;
    private Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        vector = new Vector3(-0.3f, 0, 0);
        animator = GetComponent<Animator>();
        InvokeRepeating("ComportamientoEnemigo", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AvanceEnemigo()
    {
        transform.Translate(vector * 1 * Time.deltaTime);
    }

    public void GiroEnemigo()
    {
        int i;

        i = 1;
        Debug.Log(grado);
        if (grado < 0)
            i *= -1;
        i++;
        angulo = Quaternion.Euler(0, i, 0);
        if (i != grado)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 45);
    }

    public void ComportamientoEnemigo()
    {
        try
        {
            CancelInvoke("AvanceEnemigo");
            CancelInvoke("GiroEnemigo");
        }
        catch { }
        cronometro += Random.Range(-2, 2) * 100 * Time.deltaTime;
        rutina = (int)cronometro;
        if (cronometro >= 4 || cronometro < 0)
            cronometro = 0;
        switch (rutina)
        {
            // Stop enemy
            case 0:
                animator.SetBool("walk", false);
                break;

            // Direction enemy
            case 1:
                grado = Random.Range(-45, 45);
                InvokeRepeating("GiroEnemigo", 0, 0.01f);
                break;

            // Walk enemy
            default:
                InvokeRepeating("AvanceEnemigo", 0, 0.01f);
                animator.SetBool("walk", true);
                break;
        }
    }
}

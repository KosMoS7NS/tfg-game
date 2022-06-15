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
    private int i;

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
        float anguloMax;

        anguloMax = 45;
        if (grado < 0 && i != 0)
        {
            anguloMax *= -1;
            i *= -1;
            i--;
        }
        else if(i != 0)
            i++;
        angulo = Quaternion.Euler(0, i, 0);
        Debug.Log(grado);
        if (i != grado && i != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, anguloMax);
        else
            i = 0;
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
                i = 1;
                animator.SetBool("walk", false);
                grado = Random.Range(0, 45);
                InvokeRepeating("GiroEnemigo", 0, 0.01f);
                break;

            // Walk enemy
            default:
                animator.SetBool("walk", true);
                InvokeRepeating("AvanceEnemigo", 0, 0.01f);
                break;
        }
    }
}

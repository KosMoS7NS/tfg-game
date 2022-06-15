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
        vector = new Vector3(0.1f, 0, 0);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ComportamientoEnemigo();
    }

    public void ComportamientoEnemigo()
    {
        cronometro += Random.Range(-2, 2)* 100 * Time.deltaTime;
        rutina = (int)cronometro;
        if (cronometro >= 4 || cronometro < 0)
            cronometro = 0;
        Debug.Log(Time.deltaTime);
        switch (rutina)
        {
            // Stop enemy
            case 0:
                animator.SetBool("walk", false);
                break;

            // Direction enemy
            case 1:
                grado = Random.Range(-360, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                break;

            // Walk enemy
            default:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(vector * 2 * Time.deltaTime);
                animator.SetBool("walk", true);
                break;
        }
    }
}

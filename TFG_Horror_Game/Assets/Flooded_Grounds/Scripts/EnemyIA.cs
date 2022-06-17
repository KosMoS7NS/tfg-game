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
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    private int i;
    float anguloMax;
    
    public Transform Player;
    float MoveSpeed = 0.05f;
    float MaxDist = 15.0f;
    float MinDist = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        vector = new Vector3(0.3f, 0, 0);
        animator = GetComponent<Animator>();
        InvokeRepeating("ComportamientoEnemigo", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= MinDist && Vector3.Distance(transform.position, Player.transform.position) > 5)
            SeguimientoEnemigo();
    }

    public void AvanceEnemigo()
    {
        transform.Translate(vector * 1 * Time.deltaTime);
    }

    public void GiroEnemigo()
    {
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

    public void SeguimientoEnemigo() {
        animator.SetBool("walk", false);
        animator.SetBool("run", true);

        transform.LookAt(Player);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed);

    }

    public void ComportamientoEnemigo()
    {
        // Condicional de detección de metros (5 metros de distancia)
        if (Vector3.Distance(transform.position, Player.transform.position) >= MaxDist)
        {
            animator.SetBool("run", false);
            try
            {
                CancelInvoke("AvanceEnemigo");
                CancelInvoke("GiroEnemigo");
            }
            catch
            {
                Debug.Log("Los métodos AvanceEnemigo y GiroEnemigo no están activos");
            }

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
        else 
        { 
            // Attack
        
        }
        
    }
}

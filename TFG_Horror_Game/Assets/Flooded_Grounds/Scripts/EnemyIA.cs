using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour

{
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] zombie_sounds;
    [SerializeField] private AudioClip zombieAttacking;

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
    float MaxDist = 10.0f;
    float MinDist = 2f;
    float RangeAttack = 2.5f;

    private bool textWarning;
    private bool nearPlayer;
    private bool cdAttack;
    public bool atacando;
    private CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        canvasController = FindObjectOfType<CanvasController>();
        m_AudioSource = GetComponent<AudioSource>();
        nearPlayer = false;
        textWarning = false;
        cdAttack = true;
        vector = new Vector3(0, 0, 0.3f);
        animator = GetComponent<Animator>();
        InvokeRepeating("ComportamientoEnemigo", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // Vamos comprobando que est� lejos del jugador. En el momento en que est� cerca le perseguir�
        if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist && atacando != true && Vector3.Distance(transform.position, Player.transform.position) > MinDist && canvasController.player.gameObject.activeSelf)
        {
            SeguimientoEnemigo();
            CancelInvoke("ComportamientoEnemigo");
            nearPlayer = true;

            // En caso que est� muy cerca del jugador le har� da�o, quit�ndole una vida y activando el cooldown del ataque
            if (Vector3.Distance(transform.position, Player.transform.position) <= RangeAttack && cdAttack)
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
                
                atacando = true;
                canvasController.Damage();
                cdAttack = false;
                Invoke("CoolDownAttack", 2);
            }
        }
        
        // En caso de que deje de estar cerca del jugador se avisar� de que ha huido
        else
        {
            if (nearPlayer)
            {
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                InvokeRepeating("AvanceEnemigo", 0, 0.01f);
                InvokeRepeating("ComportamientoEnemigo", 1, 3);
                canvasController.DeleteText();
                canvasController.setText("You have run away");
            }
            nearPlayer = false;
        }
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

    // FUnci�n que regula la persecuci�n al player por parte del monstruo
    public void SeguimientoEnemigo() {
        // Cambiamos la animaci�n para que el zombie salga corriendo
        animator.SetBool("walk", false);
        animator.SetBool("run", true);

        // A�adimos el sonido del zombie atacando
        m_AudioSource.clip = zombieAttacking;
        m_AudioSource.Play();

        // Hacemos que el zombie mire hacia el jugador y se mueva hacia �l
        transform.LookAt(Player);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed);

        // Hacemos que aparezca en pantalla un mensaje en rojo avisando de la cercan�a del monstruo
        if (!textWarning)
        {
            canvasController.setColorText(Color.red);
            canvasController.setPermanentText("The monster is near. Run!");
        }
        textWarning = true;
    }

    // Funci�n que regula el comportamiento del monstruo fuera de "combate"
    public void ComportamientoEnemigo()
    {
        // Condicional de detecci�n de metros (5 metros de distancia)
        /*if (Vector3.Distance(transform.position, Player.transform.position) >= MaxDist)
        {
        animator.SetBool("run", false);*/

        // A�adimos sonido al zombie de vez en cuando, de manera aleatoria.
        int soundChance = Random.Range(0, 2);
        if (soundChance == 1)
        {
            // pick & play a random zombie sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, zombie_sounds.Length);
            m_AudioSource.clip = zombie_sounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            zombie_sounds[n] = zombie_sounds[0];
            zombie_sounds[0] = m_AudioSource.clip;
        }

        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        textWarning = false;
        try
        {
            CancelInvoke("AvanceEnemigo");
            CancelInvoke("GiroEnemigo");
        }
        catch
        {
            Debug.Log("Los m�todos AvanceEnemigo y GiroEnemigo no est�n activos");
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

    // Funci�n para regular el cooldown del ataque
    public void CoolDownAttack()
    {
        cdAttack = true;
    }

    // Funci�n para destruir al monstruo
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    // Parar animaci�n de atacar
    public void StopAtack() {
        animator.SetBool("attack", false);
        atacando = false;
    }
}

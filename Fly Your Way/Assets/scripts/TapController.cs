using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public PolygonCollider2D colliderPlane;
    public PolygonCollider2D colliderShip;
    public Text playerLifeText;
    public ParticleSystem shild;
    public ParticleSystem multi;
    public ParticleSystem life;
    

    public int playerLife = 3;
    public float tapForce = 10;
    public float tiltSmooth = 2;
    public Vector3 startPos;
    public AudioSource tapSound;
    public AudioSource scoreSound;
    public AudioSource dieSound;
    

    bool IsShildActivated = false;
    public bool pointsMultiplier = false;

    Rigidbody2D rigidBody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    GameManager game;
    

    void Start()
    {
        playerLifeText.text = playerLife.ToString();
        rigidBody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -65);
        forwardRotation = Quaternion.Euler(0, 0, 55);
        game = GameManager.Instance;
        rigidBody.simulated = false;

        //Alle Partikel stoppen
        shild.Stop();
        multi.Stop();
        life.Stop();

    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        playerLifeText.text = playerLife.ToString();

        if (game.GameOver) return;
        if (pauseMenu.GameIsPaused) return;                                 // Kein Input bei aktiver Pause

        if (Input.GetMouseButtonDown(0))
        {
            rigidBody.velocity = Vector2.zero;
            transform.rotation = forwardRotation;
            rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
            tapSound.Play();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    IEnumerator ColliderSwitcherDeadZone()          //2 Sekunde den Collider deaktivieren nach Crash
    {
        colliderPlane.isTrigger = false;
        colliderShip.isTrigger = false;
        yield return new WaitForSeconds(2);
        colliderPlane.isTrigger = true;
        colliderShip.isTrigger = true;
    }

    IEnumerator ColliderSwitcherShild()             //10 Sekunden unzerstörbar nach aufsammeln des Shilds
    {
        IsShildActivated = true;
        shild.Play();
        yield return new WaitForSecondsRealtime(10);
        shild.Stop();
        IsShildActivated = false;
    }

    IEnumerator PointsMultplier()               //10 Sekunden points x3 nach PowerUp
    {
        pointsMultiplier = true;
        multi.Play();
        yield return new WaitForSecondsRealtime(10);
        multi.Stop();
        pointsMultiplier = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
            scoreSound.Play();

        }
        if (col.gameObject.tag == "DeadZone")
        {
            if (IsShildActivated == false)
            {
                playerLife--;
                StartCoroutine(ColliderSwitcherDeadZone());
                Debug.Log("You have " + playerLife + " Lives left");
                dieSound.Play();

                if (playerLife <= 0)

                {
                    rigidBody.simulated = false;
                    OnPlayerDied();
                }
            }
        }

        if (col.gameObject.tag == "PowerUpLife")
            {
            life.Play();    
            playerLife++;
            
           // Debug.Log("+1 Life! You gt now: " + playerLife);
            //lifeUpSound.Play();                   Insert SoundFile!!!
            }

            if (col.gameObject.tag == "PowerUpMultiplier")
            {
                StartCoroutine(PointsMultplier());
               // Debug.Log("*3 Multiplier");
               
                //multiplierSound.Play();                   Insert SoundFile!!!
            }
            if (col.gameObject.tag == "PowerUpShild")
            {
                StartCoroutine(ColliderSwitcherShild());    //unbesiegbar für 10Sekunden
                //Debug.Log("you got a shild fo 10 sec");
                
                //shildSound.Play();                   Insert SoundFile!!!
            }
    }

    
}
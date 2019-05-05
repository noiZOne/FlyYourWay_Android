using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSpawner : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnHeight
    {
        public float min;
        public float max;
    }

    public GameObject PowerUpLifePrefab;                    //Life PowerUp
    public GameObject PowerUpPointMultiPrefab;              //Points Multiplier PowerUp
    public GameObject PowerUpGodmodePrefab;                 //GodMode PowerUp
    int wichPower;                                          //Randomnumer for the PowerUp to spawn    1-3
    
    GameObject PowerUpToSpawn;                           //Wich PowerUp to spawn
    public TapController TapController;

    public static float shiftSpeed = 3.0f;
    public float spawnRate;                                                     // 3 by default
    public static float speedUp = 0.5f;                                         // Wert der ehöhung der Shiftgeschwindigkeit des Spiels
    public SpawnHeight spawnHeight;
    public Vector3 spawnPos;
    public Vector2 targetAspectRatio;
    public bool beginInScreenCenter;

    
   

    List<Transform> powerUps;
    float spawnTimer;
    GameManager game;
    float targetAspect;
    Vector3 dynamicSpawnPos;

  
        void Start()
        {
        
        powerUps = new List<Transform>();
            game = GameManager.Instance;
            if (beginInScreenCenter)
                SpawnPowerUp();
        }

        void OnEnable()
        {
            GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
        }

        void OnDisable()
        {
            GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
        }

        void OnGameOverConfirmed()
        {
            TapController.playerLife = 3;
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                GameObject temp = powerUps[i].gameObject;
                powerUps.RemoveAt(i);
                Destroy(temp);
            }
            if (beginInScreenCenter)
                SpawnPowerUp();
        }

        void Update()
        {
            if (game.GameOver) return;

            targetAspect = (float)targetAspectRatio.x / targetAspectRatio.y;
            dynamicSpawnPos.x = (spawnPos.x * Camera.main.aspect) / targetAspect;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                SpawnPowerUp();
                spawnTimer = 0;
            }
            ShiftPowerUp();            
        }

        void SpawnPowerUp()
        {
            WichPowerUpToSpawn();

        System.Random spawnrnd = new System.Random();                                // Take the random Number for Spawning PowerUp
        int spawnadRnd = spawnrnd.Next(1, 101);

            if (spawnadRnd % 2 == 0)                                                // Decide to Spawn or not to Spawn
            {


            GameObject powerUp = Instantiate(PowerUpToSpawn) as GameObject;
            powerUp.transform.SetParent(transform);
            powerUp.transform.localPosition = dynamicSpawnPos;
                if (beginInScreenCenter && powerUps.Count == 0)
                {
                powerUp.transform.localPosition = Vector3.zero;
                }
            float randomYPos = Random.Range(spawnHeight.min, spawnHeight.max);
            powerUp.transform.position += Vector3.up * randomYPos;
            powerUps.Add(powerUp.transform);
            }   
        }

        void ShiftPowerUp()
        {
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                powerUps[i].position -= Vector3.right * shiftSpeed * Time.deltaTime;
                if (powerUps[i].position.x < (-dynamicSpawnPos.x * Camera.main.aspect) / targetAspect)
                {
                    GameObject temp = powerUps[i].gameObject;
                    powerUps.RemoveAt(i);
                    Destroy(temp, 1f);
                }
            }
        }

        void WichPowerUpToSpawn()
        {
        System.Random rnd = new System.Random();                                // Take the random Number for PowerUp
        int adRnd = rnd.Next(1, 4);
         // Debug.Log(adRnd);
                                                                                // declaring the PowerUp wich will be spawned
            if(adRnd == 1 )
            {
            if (TapController.playerLife <= 4)
                {
                PowerUpToSpawn = PowerUpLifePrefab;
                }
            return;
            }
            if (adRnd == 2)
            {
                PowerUpToSpawn = PowerUpPointMultiPrefab;
            }

            if (adRnd == 3)
            {
                PowerUpToSpawn = PowerUpGodmodePrefab;
            }

        }
    }




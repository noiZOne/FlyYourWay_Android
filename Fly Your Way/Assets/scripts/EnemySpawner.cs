using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnHeight
    {
        public float min;
        public float max;
    }

    public GameObject EnemyPrefab;
    public static float shiftSpeed = 2.8f;
    public float spawnRate;                                                     // 3 by default
    public static float speedUp = 0.5f;                                         // Wert der ehöhung der Shiftgeschwindigkeit des Spiels
    public SpawnHeight spawnHeight;
    public Vector3 spawnPos;
    public Vector2 targetAspectRatio;
    public bool beginInScreenCenter;
    public Toggle trafficToggle;
    public bool enemyActivated;



    List<Transform> enemies;
    float spawnTimer;
    GameManager game;
    float targetAspect;
    Vector3 dynamicSpawnPos;

    public void IsEnemyActivated()
    {
        if (trafficToggle.isOn)
        {
            enemyActivated = true;
        }
        else
        {
            enemyActivated = false;
        }
    }
        void Start()
        {
            enemies = new List<Transform>();
            game = GameManager.Instance;
            if (beginInScreenCenter)
                SpawnEnemy();
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
            shiftSpeed = 1.5f;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                GameObject temp = enemies[i].gameObject;
                enemies.RemoveAt(i);
                Destroy(temp);
            }
            if (beginInScreenCenter)
                SpawnEnemy();
        }

        void Update()
        {
            if (game.GameOver) return;

            targetAspect = (float)targetAspectRatio.x / targetAspectRatio.y;
            dynamicSpawnPos.x = (spawnPos.x * Camera.main.aspect) / targetAspect;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }

            ShiftEnemy();
        }

        void SpawnEnemy()
        {
            if (enemyActivated == true)
            {
                GameObject enemy = Instantiate(EnemyPrefab) as GameObject;
                enemy.transform.SetParent(transform);
                enemy.transform.localPosition = dynamicSpawnPos;
                if (beginInScreenCenter && enemies.Count == 0)
                {
                    enemy.transform.localPosition = Vector3.zero;
                }
                float randomYPos = Random.Range(spawnHeight.min, spawnHeight.max);
                enemy.transform.position += Vector3.up * randomYPos;
                enemies.Add(enemy.transform);
            }
        }

        void ShiftEnemy()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].position -= Vector3.right * shiftSpeed * Time.deltaTime;
                if (enemies[i].position.x < (-dynamicSpawnPos.x * Camera.main.aspect) / targetAspect)
                {
                    GameObject temp = enemies[i].gameObject;
                    enemies.RemoveAt(i);
                    Destroy(temp, 1f);
                }
            }
        }
    }




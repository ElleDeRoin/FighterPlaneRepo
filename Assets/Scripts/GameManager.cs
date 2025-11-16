using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public TextMeshProUGUI scoreText;
    public GameObject coinPrefab;



    public TextMeshProUGUI livesText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;

        verticalScreenSize = cam.orthographicSize;
        horizontalScreenSize = verticalScreenSize * cam.aspect;

        score = 0;
        scoreText.text = "Score: 0";

        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();

        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("SpawnCoin", 2, 5); 
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;
    }

void CreateEnemy()
{
    float spawnY = verticalScreenSize * 0.9f; // spawn near top

    // Decide which enemy to spawn (50% chance)
    GameObject enemyToSpawn = (Random.value < 0.5f) ? enemyOnePrefab : enemyTwoPrefab;

    // Get half-size of prefab
    float halfWidth = 0.5f;
    float halfHeight = 0.5f;

    Renderer rend = enemyToSpawn.GetComponent<Renderer>();
    if (rend != null)
    {
        Vector3 size = rend.bounds.size;
        halfWidth = 0.5f * enemyToSpawn.transform.localScale.x;
        halfHeight = 0.5f * enemyToSpawn.transform.localScale.y;
    }

    int maxAttempts = 10; // avoid infinite loops
    for (int i = 0; i < maxAttempts; i++)
    {
        // Pick random X inside camera bounds, accounting for prefab width
        float spawnX = Random.Range(-horizontalScreenSize + halfWidth, horizontalScreenSize - halfWidth);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        // Check for collisions using the prefab size
        Collider[] hits = Physics.OverlapBox(spawnPos, new Vector3(halfWidth, halfHeight, 0.5f));
        if (hits.Length == 0)
        {
            Instantiate(enemyToSpawn, spawnPos, Quaternion.Euler(180, 0, 0));
            return; // successfully spawned
        }
    }

    Debug.Log("Could not find a free spot for enemy this cycle.");
}
    void SpawnCoin()
    {
        float halfWidth = 0.5f;
        float halfHeight = 0.5f;

        // Get renderer size if available
        Renderer rend = coinPrefab.GetComponent<Renderer>();
        if (rend != null)
        {
            Vector3 size = rend.bounds.size;
            halfWidth = size.x / 2f;
            halfHeight = size.y / 2f;
        }

        // X still uses camera bounds
        float spawnX = Random.Range(-horizontalScreenSize + halfWidth, horizontalScreenSize - halfWidth);

        // Y restricted to -3.5 to 0
        float spawnY = Random.Range(-3.5f + halfHeight, 0f - halfHeight);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

        // Auto-delete after 3 sec
        Destroy(coin, 3f);
    }



    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }


    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}
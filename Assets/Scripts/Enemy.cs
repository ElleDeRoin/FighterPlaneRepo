using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;  
    private GameManager gameManager;

    void Start()
    {
       
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        if (gameManager == null)
            Debug.LogWarning("GameManager not found in the scene!");
    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        
        if (whatDidIHit.CompareTag("Player"))
        {
            Player player = whatDidIHit.GetComponent<Player>();
            if (player != null)
                player.LoseALife();

            ExplodeAndDestroy();
        }
        
        else if (whatDidIHit.CompareTag("Weapons"))
        {
            Destroy(whatDidIHit.gameObject); 

            if (gameManager != null)
                gameManager.AddScore(5);

            ExplodeAndDestroy();
        }
    }

    private void ExplodeAndDestroy()
    {
        if (explosionPrefab != null)
        {
            
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionInstance, 2f); 
        }

        Destroy(gameObject); 
    }
}

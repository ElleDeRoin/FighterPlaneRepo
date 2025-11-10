using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
      // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * 8f);
        if(transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        if (whatDidIHit.gameObject.tag == "Player")
        {
            whatDidIHit.GetComponent<Player>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.gameObject.tag == "Weapons")
        {
           Destroy(whatDidIHit.gameObject);
           Instantiate(explosionPrefab, transform.position, Quaternion.identity);
           gameManager.AddScore(5);
           Destroy(this.gameObject);
           
        }
    }
}

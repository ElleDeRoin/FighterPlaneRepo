using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{

    private float playerSpeed;

    private float horizontalInput;

    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;

    private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;


    void Start()
    {
        playerSpeed = 6f;

    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        Shooting();
        //Show Tammy prevents player from leaving y bounds
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -3.5f, 0f);
        transform.position = pos;

    }

    void Movement()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        if (transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {

            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);


        }

        if (transform.position.y > verticalScreenLimit || transform.position.y < -verticalScreenLimit)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);

        }

    }

    void Shooting()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pew Pew" + verticalScreenLimit);
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        }

    }

    public void LoseALife()
    {
        Debug.Log("Player Hit!");
    }
}

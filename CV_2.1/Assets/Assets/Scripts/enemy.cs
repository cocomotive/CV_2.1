using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private int health = 3;
    private Material whiteMat;
    private Material defaultMat;
    private UnityEngine.Object explosionRef;
    private UnityEngine.Object enemyRef;
    SpriteRenderer sR;
    public float moveSpeed = 3f;
    Transform pointA, pointB;
    Vector3 localScale;
    bool movingR = true;
    Rigidbody2D rb;
    public AudioClip dead;
    AudioSource soundPlayer;


    // Start is called before the first frame update
    void Start()
    {
        enemyRef = Resources.Load("enemy");
        sR = GetComponent<SpriteRenderer>();
        whiteMat = Resources.Load("whiteFlash", typeof(Material)) as Material;
        defaultMat = sR.material;
        explosionRef = Resources.Load("Explosion");
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        pointA = GameObject.Find("pointA").GetComponent<Transform>();
        pointB = GameObject.Find("pointB").GetComponent<Transform>();
        soundPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transform.position.x > pointB.position.x)
        {
            movingR = false;
        }
        if (transform.position.x < pointA.position.x)
        {
            movingR = true;
        }

        if (movingR)
        {
            moveRight();
        }

        else
        {
            moveLeft();
        }
    }

    private void moveRight()
    {
        movingR = true;
        localScale.x = 2;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
    }

    private void moveLeft()
    {
        movingR = false;
        localScale.x = -2;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            sR.material = whiteMat;
            if (health <= 0)
            {
                killself();
            }
            else
            {
                Invoke("resetMat", 0.1f);
            }
        }
    }

    void resetMat()
    {
        sR.material = defaultMat;
    }


    private void killself()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameObject.SetActive(false);
        Invoke("respawn", 5);
    }

    void respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        enemyClone.transform.position = transform.position;
        Destroy(gameObject);
    }
}

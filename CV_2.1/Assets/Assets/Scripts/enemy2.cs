using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    private int health = 3;
    private Material whiteMat;
    private Material defaultMat;
    private UnityEngine.Object explosionRef;
    private UnityEngine.Object enemyRef;
    SpriteRenderer sR;
    public float moveSpeed = 3f;
    Transform pointC, pointD;
    Vector3 localScale;
    bool movingR = true;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        enemyRef = Resources.Load("enemy2");
        sR = GetComponent<SpriteRenderer>();
        whiteMat = Resources.Load("whiteFlash", typeof(Material)) as Material;
        defaultMat = sR.material;
        explosionRef = Resources.Load("Explosion");
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        pointC = GameObject.Find("pointC").GetComponent<Transform>();
        pointD = GameObject.Find("pointD").GetComponent<Transform>();
    }

    private void Update()
    {
        if (transform.position.x > pointD.position.x)
        {
            movingR = false;
        }
        if (transform.position.x < pointC.position.x)
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


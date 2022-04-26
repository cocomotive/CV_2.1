using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed;
    private Vector2 direction;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = direction * speed;

    }
    public void setDirection(Vector2 bulletDirection)
    {
        direction = bulletDirection;
    }

    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pj_Mov : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject pj;
    public new Vector3 respawn;
    private Rigidbody2D rb2d;
    private Animator anim;
    Blink material;
    SpriteRenderer sprite;
    public float jumpForce;
    private float speed = 3;
    private float horizontal;
    private bool grounded;
    private float lastShoot;
    bool isInmune;
    public float inmunityTime;
    public float knockBackForceX;
    public float knockBackForceY;
   


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        material = GetComponent<Blink>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        anim.SetBool("running", horizontal != 0.0f);

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);
        }

        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }

        Debug.DrawRay(transform.position, Vector3.down * 0.30f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.30f))
        {
            grounded = true;
        }

        else grounded = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
        {
            shoot();
            lastShoot = Time.time;
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    private void jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    private void shoot()
    {
        Vector3 bDirection;

        if (transform.localScale.x == 3.0f) bDirection = Vector3.right;

        else

        {
            bDirection = Vector3.left;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position + bDirection * 0.4f, Quaternion.identity);
        bullet.GetComponent<bullet>().setDirection(bDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            pj.transform.position = respawn;
        }

        if (other.gameObject.layer == 10 && !isInmune)
        {
            StartCoroutine(Inmunity());

            if (other.transform.position.x > transform.position.x)
            {
                rb2d.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }

            else
            {
                rb2d.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }

        }

        if (other.gameObject.layer == 11)
        {
            SceneManager.LoadScene(1);
        }

        if (other.gameObject.layer == 12)
        {
            SceneManager.LoadScene(0);
        }

        if (other.gameObject.layer == 13)
        {
            SceneManager.LoadScene(2);
        }

        if (other.gameObject.layer == 14)
        {
            SceneManager.LoadScene(3);
        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        isInmune = false;
        sprite.material = material.original;

    }
}

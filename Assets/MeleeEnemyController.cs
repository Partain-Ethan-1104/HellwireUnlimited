using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    public int Health = 2;
    public float moveSpeed = 10f;

    private Transform player;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = (transform.position - player.position).sqrMagnitude;
        if (playerDistance <= 100)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * moveSpeed);

            if (direction.x > 0)
            {
                sr.flipX = true;
            }

            if (direction.x < 0)
            {
                sr.flipX = false;
            }
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            Health -= 1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(15);
            }
        }
    }
}

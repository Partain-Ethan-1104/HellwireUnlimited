using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 3f;
    public GameObject projectilePrefab;
    public float shootCooldown = 1f;
    public float rushCooldown = 3f;
    public float rushDuration = 1f;
    public float rushSpeed = 7f;
    public float chargeCooldown = 20f;
    public float reverseDuration = 0.5f;

    public GameObject Boss;
    public int bossHealth = 10;

    // number of bullets
    public int num_projectile = 10;

    private Transform player;
    private float shootTimer = 0f;
    private float rushTimer = 0f;
    private float chargeTimer = 0f;
    private bool isRushing = false;
    private bool isReversing = false;

    private Vector2 rushDirection;
    private Vector2 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the player
        float playerDistance = (transform.position - player.position).sqrMagnitude;
        if (!isRushing && playerDistance <= 500)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // Check for obstacles in the path
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                // Reverse the direction if a wall is detected
                direction = -direction;
            }

            // shooting only if below 50% health 
            if ((Time.time > shootTimer) && (bossHealth <= 5))
            {
                RadialShoot();
                shootTimer = Time.time + shootCooldown;
            }

            if (Time.time > chargeTimer)
            {
                StartRush();
                chargeTimer = Time.time + chargeCooldown;
            }
        }

        if (isRushing)
        {
            Rush();
        }

        if (bossHealth == 0)
        {
            Destroy(Boss);
        }


    }



    // Other Functions:

    void RadialShoot()
    {
        for (int i = 0; i < num_projectile; i++)
        {
            float angle = i * 360f / num_projectile;
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);

            Vector2 circlePosition = new Vector2(x, y);

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position + (Vector3)circlePosition * 1.5f, Quaternion.identity);

            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Set the Projectile Velocity
            projectile.GetComponent<Rigidbody2D>().velocity = circlePosition * projectile.GetComponent<ProjectileScript>().speed;
        }
    }

    void StartRush()
    {
        isRushing = true;
        isReversing = true;
        initialPosition = transform.position;
        rushDirection = (player.position - (Vector3)initialPosition);
        rushDirection.Normalize();
        rushTimer = Time.time + rushDuration;
    }

    void reversebeforeRush()
    {
        transform.Translate(-rushDirection * moveSpeed * Time.deltaTime, Space.World);

        if (Time.time > rushTimer - reverseDuration)
        {
            isReversing = false;
        }
    }

    void Rush()
    {
        transform.Translate(rushDirection * rushSpeed * Time.deltaTime, Space.World);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rushDirection, 1f);
        if (hit.collider != null && hit.collider.tag == "Wall")
        {
            isRushing = false;
        }

        if (Time.time > rushTimer)
        {
            isRushing = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            bossHealth -= 1;
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

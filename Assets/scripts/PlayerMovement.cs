using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f; // Adjust this for smoother movement

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private Vector2 currentVelocity;

    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1;
    private float dashCounter;
    private float dashCoolCounter;

    public bool isDashingInvulnerable { get; private set; } = false;


    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        animator.SetFloat("Horizontal", inputVector.x);
        animator.SetFloat("Vertical", inputVector.y);
        animator.SetFloat("Speed", inputVector.magnitude);

        movement = Vector2.SmoothDamp(movement, inputVector * activeMoveSpeed, ref currentVelocity, smoothTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashingInvulnerable = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                isDashingInvulnerable = false;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

//Below is used to change the scene to the next room in the level.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Level Swapper 1")
        {
            LevelSwapper.Instance.sceneToMoveTo2();
        }
        if (collision.gameObject.tag == "Level Swapper 2")
        {
            LevelSwapper.Instance.sceneToMoveTo3();
        }
        if (collision.gameObject.tag == "Level Swapper 3")
        {
            LevelSwapper.Instance.sceneToMoveTo4();
        }
        if (collision.gameObject.tag == "Level Swapper 4")
        {
            LevelSwapper.Instance.sceneToMoveTo5();
        }
        if (collision.gameObject.tag == "Level Swapper 5")
        {
            LevelSwapper.Instance.sceneToMoveTo6();
        }
        if (collision.gameObject.tag == "Level Swapper 6")
        {
            LevelSwapper.Instance.sceneToMoveTo7();
        }
        if (collision.gameObject.tag == "Level Swapper 7")
        {
            LevelSwapper.Instance.sceneToMoveTo9();
        }
    }
}

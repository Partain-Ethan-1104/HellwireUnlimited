using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;
    
    private Vector2 movement;

    public int roomNumber;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    //Below is used to change the scene to the next room in the level.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Level Swapper 1")
        {
            LevelSwapper.Instance.sceneToMoveTo2();
        }
        if(collision.gameObject.tag == "Level Swapper 2")
        {
            LevelSwapper.Instance.sceneToMoveTo3();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float lifespan = 2.0f; // Adjust the lifespan as needed

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifespan)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, quaternion.identity);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }
}
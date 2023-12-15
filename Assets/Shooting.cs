using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    private int bulletsPerShot = 1; // Set the initial number of bullets per shot
    private float bulletSpreadAngle = 0f; // Set the initial spread angle
    public AudioSource gunfireAudioSource; // Reference to the AudioSource component

    public int GetBulletsPerShot()
    {
        return bulletsPerShot;
    }

    public float GetBulletSpreadAngle()
    {
        return bulletSpreadAngle;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Apply spread to the bullet's rotation
            float spread = i * bulletSpreadAngle - (bulletsPerShot - 1) * bulletSpreadAngle / 2f;
            bullet.transform.Rotate(Vector3.forward, spread);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);

            // Play gunfire sound
            if (gunfireAudioSource != null)
            {
                gunfireAudioSource.Play();
            }
        }
    }

    public void IncreaseBulletsPerShot()
    {
        Debug.Log("IncreaseBulletsPerShot method called");
        // Increase the number of bullets per shot
        bulletsPerShot++;
        Debug.Log("Bullets per shot increased to: " + bulletsPerShot);
    }

    public void SetBulletSpreadAngle(float spreadAngle)
    {
        Debug.Log("SetBulletSpreadAngle method called");
        // Set the spread angle for the bullets
        bulletSpreadAngle = spreadAngle;
        Debug.Log("Spread angle set to: " + spreadAngle);
    }
}
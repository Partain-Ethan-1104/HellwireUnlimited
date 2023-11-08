using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the weapon to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the weapon relative to the player's rotation
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }
}

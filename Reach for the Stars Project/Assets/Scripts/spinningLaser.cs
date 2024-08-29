using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningLaser : MonoBehaviour
{
    public float rotateSpeed = 100f; // Adjust this value to change the rotation speed

    // Update is called once per frame
    void Update()
    {
        // Rotate the laser around its Z-axis
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}

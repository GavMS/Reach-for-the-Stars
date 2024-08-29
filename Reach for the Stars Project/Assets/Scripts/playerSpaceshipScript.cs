using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpaceshipScript : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to change the speed of the spaceship
    public Transform spaceshipChild; // Assign the child transform in the inspector
    public GameObject explosionPref;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position; // Initialize target position
    }

    // Update is called once per frame
    void Update()
    {
        // Handle keyboard movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement != Vector2.zero)
        {
            rb.velocity = movement * speed;
            RotateSpaceship(movement); // Rotate towards movement direction
            isMoving = false; // Stop moving to the target when using keyboard
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(mousePosition.x, mousePosition.y);
            isMoving = true;
        }

        // Move towards the target position if clicked
        if (isMoving)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;
            rb.velocity = direction * speed;
            RotateSpaceship(direction); // Rotate towards the target position

            // Stop moving if close to the target
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
            }
        }
    }

    private void RotateSpaceship(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spaceshipChild.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Asteroids"))
        {
            Instantiate(explosionPref, transform.position, Quaternion.identity);
            GameObject.FindAnyObjectByType<starFinishScript>().SendMessage("RestartGame");
            Destroy(gameObject);
        }
    }
}

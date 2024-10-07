using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starFinishScript : MonoBehaviour
{
    public float amplitude = 1.0f; // The height of the movement
    public float frequency = 1.0f; // The speed of the movement

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new y position using a sine wave
        float newY = startingPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the object's position
        transform.position = new Vector3(startingPosition.x, newY, startingPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(WaitForNextLevel());
        }
    }

    IEnumerator WaitForNextLevel()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        StartCoroutine(WaitForRestart());
    }
    IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

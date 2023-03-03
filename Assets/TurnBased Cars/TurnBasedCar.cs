using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedCar : MonoBehaviour
{
    [SerializeField]Vector2 carDirection= Vector2.up;
    [SerializeField]Vector2 velocity = Vector2.zero;
    [SerializeField]float turnAngle;

    [SerializeField] float throttle;


    bool isPaused = true;

    float turnTime = 1;
    private float turnStartedTime;

    private void Update()
    {
        if (isPaused)
            return;

        velocity += carDirection * throttle * Time.deltaTime;
        carDirection = Quaternion.Euler(0, 0, turnAngle * Time.deltaTime) * carDirection;

        transform.up = carDirection;

        transform.position += (Vector3)(velocity * Time.deltaTime);

        if(Time.time - turnStartedTime > turnTime)
        {
            PauseGame();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
    }

    private void PauseGame()
    {
        isPaused = true;
    }

    void Unpause()
    {
        isPaused = false;
        turnStartedTime = Time.time;
    }

    private void OnGUI()
    {
        float yPosition = 0;
        float spacing = 50;
        if (GUI.Button(new Rect(0, (yPosition++) * spacing, 200, spacing), "Unpause"))
        {
            Unpause();
        }
        GUI.Button(new Rect(0, (yPosition ++) * spacing, 200, spacing), "Do thing");
    }
}

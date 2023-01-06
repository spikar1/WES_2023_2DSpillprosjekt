using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class MovingPlatform_Fin : MonoBehaviour
{
    public Transform endPositionTransform;
    SpriteRenderer sr;
    Rigidbody2D rb;

    [SerializeField] float travelDuration = 3, waitTime = 1;
    private bool moveToStart;

    Vector3 startPosition;
    float waitTimer = 0;
    private Vector3 targetPosition;
    float distance;

    float MoveSpeed => (distance / travelDuration) * Time.fixedDeltaTime;

    [SerializeField]
    Rigidbody2D attachedPlayer;

    private void OnValidate()
    {
        //Dont change any values when playing.
        if (Application.isPlaying)
            return;

        if (endPositionTransform == null)
            endPositionTransform = new GameObject(name + " EndPosition").transform;

        if(endPositionTransform.parent != transform) 
        {
            endPositionTransform.SetParent(transform, false);
        }
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        //Remove this as parent for endPositionTransform. This makes it not move whit this moves.
        endPositionTransform.parent = null;
        rb = GetComponentInChildren<Rigidbody2D>();

        startPosition = transform.position;
        distance = Vector2.Distance(startPosition, endPositionTransform.position);
        ToggleTarget();
    }

    private void FixedUpdate()
    {
        Vector2 movementDelta = Vector2.zero;
        if (Vector2.Distance(rb.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.zero;
            newPosition = Vector2.MoveTowards(rb.position, targetPosition, MoveSpeed);
            movementDelta = newPosition - rb.position;
            rb.MovePosition(newPosition);
        }
        else
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= waitTime)
            {
                waitTimer -= waitTime;
                ToggleTarget();
            }
        }
        if (attachedPlayer)
        {
            var velocity = attachedPlayer.velocity;
            attachedPlayer.MovePosition(attachedPlayer.position + movementDelta + velocity * Time.fixedDeltaTime);

        }
    }

    void ToggleTarget()
    {
        moveToStart = !moveToStart;

        if (moveToStart)
            targetPosition = startPosition;
        else
            targetPosition = endPositionTransform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player && Vector2.Angle(collision.contacts[0].normal, Vector2.down) < 60)
            attachedPlayer = player.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
            attachedPlayer = null;
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            startPosition = transform.position;


        Gizmos.color = new Color(0,1,0,0.4f);
        Gizmos.DrawLine(startPosition, endPositionTransform.position);

        if(Application.isPlaying)
            Gizmos.DrawCube(startPosition, sr.localBounds.size);

        Gizmos.matrix = endPositionTransform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, sr.localBounds.size.ScaleWith(sr.transform.localScale));
    }
}

static class Vector3Extensions
{
    public static Vector3 ScaleWith(this Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.x * b.x,
            a.y * b.y,
            a.z * b.z);
    }
}
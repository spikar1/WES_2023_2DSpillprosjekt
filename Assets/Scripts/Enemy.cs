using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, Vector3.zero);
    }
}

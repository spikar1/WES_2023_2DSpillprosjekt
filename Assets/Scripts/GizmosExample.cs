using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosExample : MonoBehaviour
{
    [SerializeField] bool showInPlaymode;
    private void OnDrawGizmos()
    {
        //Is we want playmode to not show our Gizmos
        if (showInPlaymode == false && Application.isPlaying)
            return;

        //Set the color of all Gizmos commands after this line
        Gizmos.color = Color.green; 

        //Draw a wireCube at the
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}

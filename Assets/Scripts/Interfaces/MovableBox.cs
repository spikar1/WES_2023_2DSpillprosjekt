using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour, IInteractable
{
    public void OnInteract(Vector2 pos)
    {
        Debug.Log("Move Box");
        transform.Translate((Vector2)transform.position - pos);
    }

}

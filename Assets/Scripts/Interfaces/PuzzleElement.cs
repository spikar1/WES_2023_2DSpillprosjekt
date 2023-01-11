using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement : MonoBehaviour, IInteractable
{
    public void OnInteract(Vector2 pos)
    {
        Debug.Log("Did a puzzle thing");
        Debug.DrawRay(transform.position, pos, Color.red, 5);
    }
}

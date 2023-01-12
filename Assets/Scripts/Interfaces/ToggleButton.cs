using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour, IInteractable
{
    public void OnInteract(Vector2 pos)
    {
        Debug.Log("Toggled something");
    }
}

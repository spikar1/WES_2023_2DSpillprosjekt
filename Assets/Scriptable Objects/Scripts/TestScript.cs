using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Test Asset")]  
public class TestScript : ScriptableObject
{
    public float someFloat;
    private int someInt;
    [SerializeField] string someString;

    [Header("Important reference")]
    public GameObject myGameObject;
}

    
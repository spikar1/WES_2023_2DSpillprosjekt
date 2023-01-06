using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools
{

    [MenuItem("Tools/SteffenFine/Place On Ground")]
    static void PlaceOnGround()
    {
        if (Selection.gameObjects.Length == 0)
            return;

        foreach (var gameObject in Selection.gameObjects)
        {
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down))
            {
                Undo.RecordObject(gameObject.transform, "Place on Ground");

                gameObject.transform.position = hit.point;
            }
        }
    }
    [MenuItem("Tools/SteffenFine/Randomize Rotation")]
    static void RandomizeRotation()
    {
        if(Selection.gameObjects.Length == 0) 
            return;

        foreach (var gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, "Randomize Rotation");

            gameObject.transform.up = Random.insideUnitCircle;
        }
    }

}

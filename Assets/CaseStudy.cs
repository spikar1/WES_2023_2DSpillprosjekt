using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CaseStudy
{
    [MenuItem("Tools/Snap To Ground #&S")]
    static void Foo()
    {
        foreach (GameObject selected in Selection.gameObjects)
        {
            //koden her inne utføres per gameobject som er selected
            Debug.Log(selected.name);
            RaycastHit2D hit = Physics2D.Raycast(selected.transform.position, Vector2.down);

            Undo.RecordObject(selected.transform, "Snap object to ground");

            if (hit)
            {
                selected.transform.position = hit.point;
            }
        }
    }
}

using UnityEditor;
using UnityEngine;

public class Tools
{
    [MenuItem("SomeMenyGroup/Do Foo")]
    static void Foo()
    {
        // Get all objects of Type Enemy (This is a MonoBehaviour)
        var enemies = GameObject.FindObjectsOfType<Enemy>();

        // Create an array to put our new selection of GameObjects in
        GameObject[] newSelection = new GameObject[enemies.Length];

        // Go through all the enemies we found
        for (int i = 0; i < enemies.Length; i++)
        {
            // Add the gameObject of each enemy into our new selection
            newSelection[i] = enemies[i].gameObject;
        }

        // Overwrite the Selection in Unity
        Selection.objects = newSelection;
    }
    [MenuItem("Tools/SteffenFine/Randomize Rotation #&G")]
    static void RandomizeRotation()
    {
        // if no Transforms are selected, do nothing
        if (Selection.transforms.Length == 0)
            return;

        //Iterate through all selected transforms
        foreach (Transform transform in Selection.transforms)
        {
            //Declare to Unity that a change will be made, and name it
            Undo.RecordObject(transform, "Randomize Rotation");

            //Apply the actual change
            transform.up = Random.insideUnitCircle;
        }
    }
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

    [MenuItem("Tools/SteffenFine/Place On Ground 2.0")]
    static void PlaceOnGround2()
    {
        if (Selection.transforms.Length <= 0)
            return;

        foreach (var transform in Selection.transforms)
        {
            //Deklarerte en hit variabel
            RaycastHit2D hit;
            // assigne den
            hit = Physics2D.Raycast(transform.position, Vector2.down);

            // hit != null
            if (hit)
            {
                Undo.RecordObject(transform, "Place on Ground 2.0ofgnsdgovsd");

                Debug.DrawLine(transform.position, hit.point, Color.blue, 3);

                transform.position = hit.point;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlaceSelectionInCircle : ScriptableWizard
{
    [SerializeField] Vector2 size = Vector2.one;
    [SerializeField] float rotationOffset = 0;

    [MenuItem("Tools/SteffenFine/Place selection in circle")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<PlaceSelectionInCircle>("Place In Circle", "Done", "Update Circle");
    }

    private void OnWizardOtherButton()
    {
        //If no objects are selected, abort
        int selectionCount = Selection.count;
        if (selectionCount == 0)
            return;
        
        //Make scene achnowledge the changes we will peform, and make it undoable
        Undo.RecordObjects(Selection.transforms, "PlaceInCircle");
        
        AlignGameObjects();
    }
    private void AlignGameObjects()
    {
        //If no objects are selected, abort
        int selectionCount = Selection.count;
        if (selectionCount == 0)
            return;

        //Calculate the center of all objects.
        Vector3 total = Vector3.zero;
        Selection.transforms.ToList().ForEach(t => total += t.position);
        Vector3 centerPosition = total / selectionCount;

        for (int i = 0; i < selectionCount; i++)
        {
            //Find the angle in radians
            float angle = (float)i / selectionCount * Mathf.PI * 2;

            //Add the rotational offset (Converted to radians)
            angle += rotationOffset * Mathf.Deg2Rad;

            //Use sine and cosine to convert angle to X and Y position, and size the circle 
            float x = Mathf.Sin(angle) * size.x;
            float y = Mathf.Cos(angle) * size.y;


            //Apply new position to each object in the loop
            Selection.transforms[i].position = new Vector3(x, y, 0) + centerPosition;
        }
    }

    private void OnWizardUpdate()
    {
        helpString = "GameObjects selected: " + Selection.count;

        AlignGameObjects();
    }

    
}

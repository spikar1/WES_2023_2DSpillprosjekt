using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_thisOne : MonoBehaviour
{
    private float interactRadius = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, interactRadius))
            {

                Debug.Log("coll" + collider.gameObject.name);
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if(interactable != null)
                {
                    interactable.OnInteract(transform.position);
                }
            }
        }
    }
}

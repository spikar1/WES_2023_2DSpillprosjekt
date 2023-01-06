using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOJump(Vector3.zero, 5, 2, 1).SetDelay(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingDoTween : MonoBehaviour
{
    [SerializeField]
    List<Rigidbody2D> objectsToMove = new List<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        transform.DOJump(Vector3.zero, 5, 2, 1).SetDelay(1);

        for (int i = 0; i < objectsToMove.Count; i++)
        {
            Rigidbody2D obj = objectsToMove[i];
            obj.DOMoveY(4, 1).SetEase(Ease.InOutSine).SetRelative(true).SetDelay((float)i * .2f).SetLoops(-1, LoopType.Yoyo);
        }

    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBoxesMove : MonoBehaviour
{
    [SerializeField]
    Transform _tweened, _unTweened;

    [SerializeField]
    float _moveDuration;


    void Start()
    {
        MoveTweenedBox();
        MoveUnTweenedBox();
    }

    //A standard animation curve used to add ease
    AnimationCurve _animCurve = AnimationCurve.EaseInOut(0,0,1,5);

    private void MoveUnTweenedBox()
    {
        //Start the Coroutine
        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {
        //Save old position for use in animation
        Vector3 oldPosition = _unTweened.position;

        //Iterate for the duration of the move animation
        for (float f = 0; f < _moveDuration; f+= Time.deltaTime)
        {
            //Calculate a position from a Curve
            Vector3 newPosition = oldPosition + Vector3.down * _animCurve.Evaluate(f / _moveDuration);
            //Set the new position to our Transform
            _unTweened.position = newPosition;
            //Wait until next frame
            yield return null;
        }
        //When animation is done we make sure the new position is correct
        _unTweened.position = oldPosition + Vector3.down * 5;
    }

    private void MoveTweenedBox()
    {
        //DoLocalMoveY creates a "Tween" that moves our transform
        //SetEase defines what method of ease we use (From a list of options)
        _tweened.DOLocalMoveY(-5, _moveDuration).SetEase(Ease.InOutSine);
    }
}

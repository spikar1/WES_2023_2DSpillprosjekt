using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    [SerializeField] ParticleSystem _particlesPrefab;
    [SerializeField] Transform _moverBox;

    void Start()
    {
        _moverBox.DOLocalMove(Vector3.right * 5, 2)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() => SpawnParticles());
    }

    private void SpawnParticles()
    {
        Instantiate(_particlesPrefab, _moverBox.position, Quaternion.identity);
        _moverBox.DOPunchScale(Vector3.one * .4f, 1f, 3,.6f).SetEase(Ease.OutCubic);
        _moverBox.DORotate(_moverBox.rotation.eulerAngles + Vector3.forward * 90, 1.5f, RotateMode.FastBeyond360).SetEase(Ease.OutElastic);
    }
}

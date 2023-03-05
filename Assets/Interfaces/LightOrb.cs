using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : MonoBehaviour, IDamageable
{
    public void OnDamage(Vector2 origin, int damageValue)
    {
        transform.DOPunchPosition((Vector2)transform.position - origin, .5f);
        //transform.position += transform.position - (Vector3)origin;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCrate : MonoBehaviour, IDamageable
{
    void OnDestroy()
    {
        //Todo: add fx for when crate is destroyed
        Destroy(gameObject);
    }

    public void OnDamage(Vector2 origin, int damageValue)
    {
        OnDestroy();
        throw new System.NotImplementedException();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void OnTakeDamage(int damageValue, Vector2 direction);
}

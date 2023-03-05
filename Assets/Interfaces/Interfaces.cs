using UnityEngine;

public interface IDamageable
{
    public void OnDamage(Vector2 origin, int damageValue);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int attackPower;
    [SerializeField]
    private float radius = 5;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var col in Physics2D.OverlapCircleAll(transform.position, radius))
            {
                var damageable = col.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.OnTakeDamage(attackPower, (col.transform.position - transform.position).normalized);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

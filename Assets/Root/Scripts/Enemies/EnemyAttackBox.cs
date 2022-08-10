using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public Transform damageOrigin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RegistDamage(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        RegistDamage(collision);
    }
    void RegistDamage(Collider2D collision)
    {
        var obj = collision.gameObject.GetComponent<PlayerHealth>();
        if (obj != null)
        {
            if (damageOrigin != null)
            {
                obj.GetHit((obj.transform.position - damageOrigin.position).normalized);
            }
            else
            {
                obj.GetHit((obj.transform.position - transform.position).normalized);
            }

        }
    }
}

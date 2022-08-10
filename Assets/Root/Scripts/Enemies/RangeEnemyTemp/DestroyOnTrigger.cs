using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
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
            obj.GetHit((obj.transform.position - transform.position).normalized);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Obstacles")
        {
            Destroy(gameObject);
        }
    }
}

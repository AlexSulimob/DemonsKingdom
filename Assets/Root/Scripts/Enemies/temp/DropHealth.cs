using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHealth : MonoBehaviour
{
    public bool isDestroy = false;
    public EnemyHealth enemyHealth;
    public bool isWithHealth;
    public GameObject HealthPrefab;
    void Start()
    {
        enemyHealth.dead += delegate ()
        {
            if (Random.Range(0,100)<=2 || isWithHealth)
            {
                Instantiate(HealthPrefab, transform.position, Quaternion.identity);
            }
            if (isDestroy)
            {
                Destroy(gameObject);
            }

        };
    }


}

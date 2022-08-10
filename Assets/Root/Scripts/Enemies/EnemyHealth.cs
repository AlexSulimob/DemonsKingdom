using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour
{
    public event Action<Vector2> isGetingHit;
    public event Action dead;
    public float maxHealth = 2;
    [HideInInspector]
    public float currentHealth;
    public EnemiMiliStates enemiMiliStates;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit(Vector2 directionHit)
    {
        if (enemiMiliStates!=null)
        {
            if (enemiMiliStates.isInvincible)
            {
                return;
            }
        }
        currentHealth--;
        if (isGetingHit != null)
        {
            isGetingHit(directionHit);  
        }

        if (currentHealth <= 0)
        {
            if (dead != null)
            {
                dead();
            }

        }
    }
}

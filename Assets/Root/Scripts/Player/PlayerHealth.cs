using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action<Vector2> isGetingHit;
    public event Action RestoreHealth;
    public event Action dead;
    public float maxHealth = 2;
    float _currentHealth;
    public float CurrentHealth { get => _currentHealth; }
    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void GetHit(Vector2 directionHit)
    {
        if (Singleton<PlayerStates>.Instance.isInvincible)
        {
            return;
        }
        _currentHealth--;
        if (isGetingHit != null)
        {
            isGetingHit(directionHit);
        }

        if (_currentHealth <= 0)
        {
            if (dead != null)
            {
                dead();
            }

        }
    }
    public void RestoreHp()
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth++;
        }

        if (RestoreHealth != null)
        {
            RestoreHealth();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : Singleton<PlayerStates>
{
    [HideInInspector]
    public pStates currentState;
    [HideInInspector]
    public bool isInvincible = false;
    private void Update()
    {
        //Debug.Log(currentState);
    }
    public enum pStates
    {
        Walking,
        Dashing,
        Attacking,
        GettingDamage,
        Dead
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiMiliStates : MonoBehaviour
{

    public States CurrentState { get ; set ; }
    [HideInInspector]
    public bool isInvincible = false;

    private void Update()
    {

        //Debug.Log(CurrentState);
    }
    public enum States
    {
        RandomMovement,
        Follow,
        Attack,
        GettingHit,
        Dead
    }
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}

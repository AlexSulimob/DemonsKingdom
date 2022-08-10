using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationEvents : MonoBehaviour
{
    public event Action EndAttack;
    public event Action<Vector2> InitRangeAttack;
    public void InitEndAttack()
    {
        if (EndAttack != null)
        {
            EndAttack();
        }
    }

    public void AimTarget(){
        if(InitRangeAttack != null) {
            InitRangeAttack(Singleton<PlayerStates>.Instance.transform.position);
        }
    }
}

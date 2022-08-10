using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RangeEnemyAttackTargetPos : MonoBehaviour
{
    [HideInInspector]
    public Vector2 targetPos;

    EnemiMiliStates miliStates;
    bool _isAttackInit = false;
    void Start()
    {
        miliStates = GetComponent<EnemiMiliStates>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void AimTarget(){
        targetPos = Singleton<PlayerStates>.Instance.transform.position;
    }
}

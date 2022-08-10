using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
public class EmptyState : IState
{
    Func<string> debugOutput;
    public EmptyState(Func<string> debuging)
    {
        debugOutput = debuging;
    }

    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }

    public void Tick()
    {

    }


}

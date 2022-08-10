using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    public event Action GateOpen;
    public BoxCollider2D BlockCollision;
    public void GateOpened()
    {
        if (GateOpen != null)
        {
            BlockCollision.enabled = false;
            GateOpen();
        }

    }
}

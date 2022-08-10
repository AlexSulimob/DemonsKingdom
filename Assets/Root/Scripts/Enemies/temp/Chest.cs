using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Chest : MonoBehaviour
{
    public event Action ChestOpen;
    public GameObject dropItem;
    public void ChestOpened()
    {
        if (ChestOpen!=null)
        {
            dropItem.SetActive(true);
            ChestOpen();
        }

    }
}

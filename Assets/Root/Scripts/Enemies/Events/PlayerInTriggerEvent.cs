using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInTriggerEvent : MonoBehaviour
{
    public event Action PlayerInTrigger;
    public bool TriggerAlways = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PlayerInTrigger != null)
            {
                PlayerInTrigger();
            }
            
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!TriggerAlways)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            if (PlayerInTrigger != null)
            {
                PlayerInTrigger();
            }

        }
    }
    
}

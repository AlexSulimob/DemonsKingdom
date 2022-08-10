using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = Singleton<PlayerStates>.Instance.gameObject.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAndRestore(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckAndRestore(collision);
    }
    void CheckAndRestore(Collider2D collision)
    {

        if (playerHealth.CurrentHealth<playerHealth.maxHealth)
        {
            playerHealth.RestoreHp();
            Destroy(gameObject);
        }
        

    }
}

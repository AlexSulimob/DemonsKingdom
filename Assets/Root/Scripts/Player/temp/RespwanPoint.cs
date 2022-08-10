using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Singleton<GameManager>.Instance.playerSave.currentRespwanPoint = transform.position;
        Singleton<GameManager>.Instance.MakeSave();
    }
}

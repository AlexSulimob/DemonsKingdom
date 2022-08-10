using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public AnimationEvents animEvent;
    public GameObject bulletPrefab;
    Vector2 _directionAnimPos;
    private void Start() {
        animEvent.InitRangeAttack += targetPos => _directionAnimPos = targetPos;
    }
    private void OnEnable()
    {
        GameObject obj = Instantiate(bulletPrefab, transform.position,Quaternion.Euler(0,0,0));
        var b = obj.GetComponent<Bullet>();
        if(b != null){
            b.TargetPost = _directionAnimPos;
        }
    }
}

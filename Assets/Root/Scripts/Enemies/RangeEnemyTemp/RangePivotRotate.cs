using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePivotRotate : MonoBehaviour
{
    public AnimationEvents animEvent;
    Vector2 _directionAnimPos;
    public Transform baseObj;

    Vector2 direction;
    private void Start() {
        animEvent.InitRangeAttack += targetPos => _directionAnimPos = targetPos;
    }
    void Update()
    {
        
        direction = (_directionAnimPos - (Vector2)baseObj.position).normalized;

        if (direction == Vector2.zero)
        {
            return;
        }
        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0.0f) { angle = 360.0f - angle; }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

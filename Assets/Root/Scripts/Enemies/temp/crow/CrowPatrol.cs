using System.Collections;
using UnityEngine;

namespace Crow
{
    public class CrowPatrol : IState
    {
        IEnumerator coroutine;
        CrowState crowState;
        Rigidbody2D rb; 

        RaycastHit2D[] Raycasthits = new RaycastHit2D[15];
        Vector2 _target;
        bool _wayFound = false;
        private float _speed = 50f;
        private float _distanceCheck = 4f;
        private float reachedDistance = 1.3f;

        private int checkCounter = 0;

        public ContactFilter2D Filter2D { get ; set ; }
        public CrowPatrol(Rigidbody2D rb, CrowState crowState)
        {
            this.crowState = crowState;
            this.rb = rb;
        }
        public void OnEnter()
        {
            checkCounter = 0;
            coroutine = checkPath();
            Singleton<GameManager>.Instance.StartCoroutine(coroutine);

            Debug.Log("patrol");
        }

        public void OnExit()
        {
            rb.velocity = Vector2.zero;

        }

        public void Tick()
        {
            if (_wayFound)
            {
                Vector2 direction = _target - rb.position;
                rb.velocity = direction.normalized * _speed * Time.deltaTime;
            }
            Debug.Log(checkCounter);
            if (Vector2.Distance(rb.position, _target) <= reachedDistance || checkCounter  >= 10)
            {
                crowState.CurrentState = CrowState.CrowStates.Idle;
            }
        }
        void CheckPath(){
            
            Vector2 RndDirection = getRandomDirection();
            Vector2 RndPoint = rb.position + RndDirection * _distanceCheck;

            Vector2 direction = RndPoint - rb.position;
            int hits = rb.Cast(direction.normalized, Filter2D, Raycasthits, Vector2.Distance(rb.position, RndPoint));

            if (hits == 0)
            {      
                _target = RndPoint;
                _wayFound = true;
            }
            else if (checkCounter <= 10 && hits != 0)
            { 
                checkCounter++;
                CheckPath(); 
            }
            
            
        }
        IEnumerator checkPath(){
            CheckPath();
            yield return null;
        }
        Vector2 getRandomDirection()
        {
            float minValue = 0f;
            float maxValue = 1f;
            float x_value;
            float y_value;
            Vector2 startPos = Vector2.zero;
            var high_x = Random.Range(startPos.x + minValue, startPos.x + maxValue);
            var low_x = Random.Range(startPos.x - minValue, startPos.x - maxValue);

            var high_y = Random.Range(startPos.y + minValue, startPos.y + maxValue);
            var low_y = Random.Range(startPos.y - minValue, startPos.y - maxValue);

            x_value = Random.Range(0, 2) == 1 ? high_x : low_x;
            y_value = Random.Range(0, 2) == 1 ? high_y : low_y;

            return new Vector2(x_value, y_value).normalized;
        }
    }


}


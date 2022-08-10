using System.Collections;
using UnityEngine;

namespace Crow
{
    class CrowIdle : IState
    {
        IEnumerator idleCoroutine;
        float idleTime = 5f;
        CrowState crowState;
        Rigidbody2D rb;

        public CrowIdle(Rigidbody2D rb, CrowState crowState)
        {
            
            this.rb = rb;
            this.crowState = crowState;
        }

        public void OnEnter()
        {
            Debug.Log("idle");
            rb.velocity = Vector2.zero;
            idleCoroutine = WaitIdle();
            Singleton<GameManager>.Instance.StartCoroutine(idleCoroutine);
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
        }
        IEnumerator WaitIdle(){
            yield return new WaitForSeconds(idleTime);
            crowState.CurrentState = CrowState.CrowStates.Patrol; 
        }
    }


}


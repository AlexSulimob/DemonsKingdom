using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Crow
{
    public class CrowMoving : MonoBehaviour
    {
        public ContactFilter2D filter;
        Rigidbody2D rb;
        StateMachine stateMachine;
        CrowState crowState;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            stateMachine = new StateMachine();
            crowState = GetComponent<CrowState>();

            var idleState = new CrowIdle(rb, crowState);
            var patrolState = new CrowPatrol(rb, crowState);
            var followState = new CrowFollowState(rb);
            var attackingState = new CrowAttacking();
            var gettingHitState = new CrowGettingHit();
            var deadState = new CrowIsDead();

            patrolState.Filter2D = filter;

            stateMachine.AddAnyTransition(idleState, () => crowState.CurrentState == CrowState.CrowStates.Idle);
            stateMachine.AddAnyTransition(patrolState, () => crowState.CurrentState == CrowState.CrowStates.Patrol);
            stateMachine.AddAnyTransition(attackingState, () => crowState.CurrentState == CrowState.CrowStates.Attacking);
            stateMachine.AddAnyTransition(gettingHitState, () => crowState.CurrentState == CrowState.CrowStates.GettingHit);
            stateMachine.AddAnyTransition(deadState, () => crowState.CurrentState == CrowState.CrowStates.Dead);

            stateMachine.SetState(idleState);
        }

        void Update()
        {
            
        }
        private void FixedUpdate() {
            stateMachine.Tick();
        }
    }
    class CrowFollowState : IState
    {
        Rigidbody2D rigidbody2D;
        public CrowFollowState(Rigidbody2D rigidbody2D)
        {
            this.rigidbody2D = rigidbody2D;
        }
        public void OnEnter()
        {
            Debug.Log("Follow");
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
    class CrowAttacking : IState
    {
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
    class CrowGettingHit : IState
    {
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
    class CrowIsDead : IState
    {
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


}


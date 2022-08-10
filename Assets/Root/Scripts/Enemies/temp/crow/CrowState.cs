using UnityEngine;

namespace Crow
{
    public class CrowState : MonoBehaviour {
        public CrowStates CurrentState { get; set; }
        private void Start() {
            CurrentState = CrowStates.Idle;
        }
        public enum CrowStates{
            Idle,
            Patrol,
            Following,
            Attacking,
            GettingHit,
            Dead
        }
    }


}


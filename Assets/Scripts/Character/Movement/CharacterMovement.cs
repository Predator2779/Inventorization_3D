using UnityEngine;
using UnityEngine.AI;

namespace Character.Movement
{
    public class CharacterMovement : IMovable
    {
        private NavMeshAgent _agent;
        
        public CharacterMovement(NavMeshAgent agent) => _agent = agent;
        public void MoveTo(Vector3 position) => _agent.SetDestination(position);
    }
}
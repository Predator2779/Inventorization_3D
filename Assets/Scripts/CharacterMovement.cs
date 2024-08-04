using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour, IMovable
{
    private NavMeshAgent _agent;
    private IMoveInput _moveInput;

    [Inject]
    private void Construct(IMoveInput input) => _moveInput = input;
    private void Awake() => _agent = GetComponent<NavMeshAgent>();
    public void MoveTo(Vector3 position) => _agent.SetDestination(position);
}
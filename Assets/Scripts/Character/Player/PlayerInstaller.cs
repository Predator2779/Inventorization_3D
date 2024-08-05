using Character.Input;
using Character.Movement;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Character.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public override void InstallBindings()
        {           
            Container
                .Bind<IMovable>()
                .To<CharacterMovement>()
                .AsSingle()
                .WithArguments(_navMeshAgent);

            Container
                .Bind<IMoveInput>()
                .To<MoveInputClick>()
                .AsSingle()
                .WithArguments(_playerCamera);
        }
    }
}
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class TestInstaller : MonoInstaller
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private CharacterMovement _movement;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IMoveInput>()
                .To<MoveInputClick>()
                .AsSingle()
                .WithArguments(_playerCamera, _movement);
        }
    }
}
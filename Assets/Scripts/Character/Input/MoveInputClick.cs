using Character.Movement;
using UniRx;
using UnityEngine;

namespace Character.Input
{
    public class MoveInputClick : IMoveInput
    {
        private Camera _playerCamera;
        private IMovable _movable;

        public MoveInputClick(Camera camera, IMovable movable)
        {
            _playerCamera = camera;
            _movable = movable;

            Observable
                .EveryUpdate()
                .Where(_ => IsMoving())
                .Subscribe(_ => _movable.MoveTo(GetMousePosition()));
        }

        private bool IsMoving() => UnityEngine.Input.GetMouseButtonDown(0);
        private Vector3 GetMousePosition()
        {
            Ray ray = _playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Vector3 position = Vector3.zero;
        
            if (Physics.Raycast (ray, out RaycastHit hit))
                position = hit.point;

            return position;
        }
    }
}
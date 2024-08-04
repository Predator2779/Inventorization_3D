using UniRx;
using UnityEngine;
using Zenject;

public class MoveInputClick : IMoveInput
{
    private Camera _playerCamera;
    private IMovable _movable;

    public MoveInputClick(Camera camera, IMovable movable)
    {
        _playerCamera = camera;
        _movable = movable;

        Observable.EveryUpdate()
            .Where(_ => IsMove())
            .Subscribe(_ => _movable.MoveTo(GetMousePosition()));
    }

    private bool IsMove() => Input.GetMouseButtonDown(0);
    private Vector3 GetMousePosition()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 position = Vector3.zero;
        
        if (Physics.Raycast (ray, out RaycastHit hit))
            position = hit.point;

        return position;
    }
}
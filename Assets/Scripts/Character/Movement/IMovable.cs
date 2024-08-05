using UnityEngine;

namespace Character.Movement
{
    public interface IMovable
    {
        public void MoveTo(Vector3 position);
    }
}
using Character.Input;
using Character.Movement;
using UnityEngine;
using Zenject;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [Inject]
        private void Construct(IMovable movable, IMoveInput input)
        {
        }
    }
}
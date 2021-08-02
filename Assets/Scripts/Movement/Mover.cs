using UnityEngine;
using Descension.Actors;
using UnityEngine.InputSystem;

namespace Descension.Movement
{
    public abstract class Mover : MonoBehaviour, IActorMover
    {
        public virtual void CalcDir()
        {

        }
        public virtual void CalcDir(InputAction.CallbackContext context)
        {

        }
        public abstract void Move();
    }
}
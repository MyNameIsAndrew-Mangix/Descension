using System;
using UnityEngine;
using Descension.Core;
namespace Descension.Actors
{
    public class Player : MonoBehaviour
    {
        private CameraFollow _cameraFollow;
        private IActorMover iActorMover;
        private void Start()
        {
            _cameraFollow = FindObjectOfType<CameraFollow>();
            _cameraFollow.UpdateFollowTarget(this.transform);
        }
        private void FixedUpdate()
        {
            iActorMover.Move();
        }

        internal void GetMovingActor(IActorMover actorMover)
        {
            iActorMover = actorMover;
        }
    }
}
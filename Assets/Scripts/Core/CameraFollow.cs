using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Descension.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        void Update()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }

        public void UpdateFollowTarget(Transform transform)
        {
            _target = transform;
        }
    }
}
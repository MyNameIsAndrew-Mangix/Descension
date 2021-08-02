using UnityEngine;

namespace Descension.Combat
{
    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private Animation _attackAnim;

        public abstract void Attack();
    }
}
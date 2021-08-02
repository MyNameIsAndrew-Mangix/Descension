using UnityEngine;
using Descension.Movement;
using Descension.Combat;
using System;

namespace Descension.Actors
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [Range(0, 30)] protected int health;
        [SerializeField] [Range(0, 3)] protected int damage;
        protected Player _player { get; private set; }
        protected BoxCollider2D _boxCollider2D { get; private set; }
        protected AudioSource _deathSound { get; private set; }
        protected AudioSource _attackSound { get; private set; }
        [SerializeField] protected Mover _mover;
        [SerializeField] protected Attacker _attacker;

        protected bool isAlive;

        private void Start()
        {
            if (_boxCollider2D == null)
                Debug.LogError("_boxCollider2D is NULL");
            if (_deathSound == null)
                Debug.LogError("_deathSound is NULL");
            if (_attackSound == null)
                Debug.LogError("_attackSound is NULL");
            if (_mover == null)
                Debug.LogError("_mover is NULL");
            if (_attacker == null)
                Debug.LogError("_attacker is NULL");
        }

        private void Update()
        {
            _mover.Move();
            //if in range, _attacker.Attack();
        }

        protected void TakeDamage()
        {
            //lower hp();
            //if hp is >= 0, Die();
        }

        protected void Die()
        {
            //death rattle
            //set inactive
        }
    }
}
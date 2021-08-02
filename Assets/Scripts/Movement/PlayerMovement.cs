using System.Collections;
using System.Collections.Generic;
using Descension.Actors;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Descension.Movement
{
    public class PlayerMovement : Mover
    {
        [SerializeField] private Rigidbody2D _rb2D;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Player _player;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _yVelocity;
        [SerializeField] BoxCollider2D _collider;

        [SerializeField] private LayerMask mask;

        private float skin = .01f;
        private float distToGround;
        [SerializeField] private Vector2 _dir;
        private Vector2 _cachedDir;
        private bool hasJumped = false;

        private void Awake()
        {
            _player.GetMovingActor(this);
        }
        private void Start()
        {
            if (_player == null)
                Debug.LogError("_player is NULL");
            if (_rb2D == null)
                Debug.LogError("_rb2D is NULL");
            distToGround = _collider.bounds.extents.y;

        }

        public override void CalcDir(InputAction.CallbackContext context)
        {
            _dir = context.ReadValue<Vector2>();
            _cachedDir = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            hasJumped = context.ReadValueAsButton();
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Terrain"))
                _yVelocity = 0;
        }

        public override void Move()
        {
            if (IsGrounded())
            {
                _yVelocity = 0;
                if (hasJumped)
                    Jump();
            }
            else
            {
                if (!hasJumped)
                    Fall();
            }

            CheckIfStuckOnWall(IsRunningIntoWallLeftSide(), IsRunningIntoWallRightSide());

            transform.Translate(_dir * (_speed * Time.deltaTime));

            Vector3 velocity = new Vector3(0, _yVelocity, 0);
            _rb2D.velocity = velocity;
        }

        private void CheckIfStuckOnWall(bool leftSideStick, bool rightSideStick)
        {
            if (leftSideStick && !IsGrounded())
            {
                GetUnstuck();
                _dir.x = 0;
            }
            else if (rightSideStick && !IsGrounded())
            {
                GetUnstuck();
                _dir.x = 0;
            }
            else if (_cachedDir.x != 0)
                _dir = _cachedDir;
        }

        private void GetUnstuck()
        {
            float tmp = 0.5f;
            _dir.y = tmp;
            transform.Translate(_dir * (_speed * Time.deltaTime));
        }

        private void FastFall()
        {
            throw new NotImplementedException();
        }

        private void Fall()
        {
            if (_yVelocity > 0 - _gravity * 9)
                _yVelocity -= _gravity;
        }

        public void Jump()
        {
            _yVelocity = _jumpHeight;
            _rb2D.AddForce(Vector2.up * _yVelocity);
        }
        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.1f, mask);
            Debug.Log((bool)hit);
            return hit;
        }

        private bool IsRunningIntoWallRightSide()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(skin * -2);

            RaycastOrigin raycastOrigins = new RaycastOrigin();
            raycastOrigins.bottom = new Vector2(bounds.center.x, bounds.min.y);
            raycastOrigins.top = new Vector2(bounds.center.x, bounds.max.y);
            raycastOrigins.center = new Vector2(bounds.center.x, bounds.center.y);


            bool topRight = Physics2D.Raycast(raycastOrigins.top, Vector2.right, distToGround + 0.01f, mask);
            bool botRight = Physics2D.Raycast(raycastOrigins.bottom, Vector2.right, distToGround + 0.01f, mask);
            bool centerRight = Physics2D.Raycast(raycastOrigins.center, Vector2.right, distToGround + 0.01f, mask);

            DrawRays(topRight, botRight, centerRight, Vector2.right, raycastOrigins);
            return (topRight || botRight || centerRight) && _dir.x > 0;
        }

        private void DrawRays(bool topRight, bool botRight, bool centerRight, Vector2 direction, RaycastOrigin origin)
        {
            if (topRight)
                Debug.DrawRay(origin.top, direction, Color.yellow);
            else
                Debug.DrawRay(origin.top, direction, Color.red);

            if (centerRight)
                Debug.DrawRay(origin.center, direction, Color.yellow);
            else
                Debug.DrawRay(origin.center, direction, Color.green);
            if (botRight)
                Debug.DrawRay(origin.bottom, direction, Color.yellow);
            else
                Debug.DrawRay(origin.bottom, direction, Color.magenta);
        }

        private bool IsRunningIntoWallLeftSide()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(skin * -2);

            RaycastOrigin raycastOrigins = new RaycastOrigin();
            raycastOrigins.bottom = new Vector2(bounds.center.x, bounds.min.y);
            raycastOrigins.top = new Vector2(bounds.center.x, bounds.max.y);
            raycastOrigins.center = new Vector2(bounds.center.x, bounds.center.y);


            bool topLeft = Physics2D.Raycast(raycastOrigins.top, Vector2.left, distToGround + 0.01f, mask);
            bool botLeft = Physics2D.Raycast(raycastOrigins.bottom, Vector2.left, distToGround + 0.01f, mask);
            bool centerLeft = Physics2D.Raycast(raycastOrigins.center, Vector2.left, distToGround + 0.01f, mask);
            return (topLeft || botLeft || centerLeft) && _dir.x < 0;
        }

        struct RaycastOrigin
        {
            public Vector2 bottom, top;
            public Vector2 left, right;
            public Vector2 center;
        }
    }
}
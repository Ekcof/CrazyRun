using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nameofthegame.Inputs
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float gravityPull;
        private Rigidbody playerRigidbody;
        private float horizontal;
        private float vertical;
        private Vector3 movement;
        private float disstanceToTheGround;
        private float moveDown;
        private bool isGrounded;

        void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            disstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        }

        public void Move(Vector3 movement)
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, disstanceToTheGround + 0.1f);
            if (isGrounded)
            {
                moveDown = 0;
            }
            else
            {
                moveDown = gravityPull * Physics.gravity.y * Time.deltaTime;
            }
            movement = new Vector3 (movement.x, moveDown, movement.z);
            playerRigidbody.AddForce(movement * speed, ForceMode.Impulse);
            if (playerRigidbody.velocity.magnitude > speed)
            {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * speed;
            }
        }
#if UNITY_EDITOR
        [ContextMenu("Reset speed")]
        public void ResetSpeed()
        {
            speed = 2;
        }
#endif
    }
}

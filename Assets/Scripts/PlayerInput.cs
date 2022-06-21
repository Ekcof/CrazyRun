using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nameofthegame.Inputs
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private float horizontal;
        private float vertical;
        private PlayerMovement playerMovement;
        private Vector3 movement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            horizontal = Input.GetAxis(GameNamespace.HORIZONTAL_AXIS);
            vertical = Input.GetAxis(GameNamespace.VERTICAL_AXIS);
            movement = new Vector3(horizontal, 0, vertical);
        }

        private void FixedUpdate()
        {
            playerMovement.Move(movement);
        }
    }
}
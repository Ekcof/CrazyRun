using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float maxSpeed = 10f;
    private TouchControl touchControl;
    private Rigidbody rigidBody;
    private bool isGrounded;
    private float distanceToTheGround;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        touchControl = Camera.main.GetComponent<TouchControl>();
        distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (touchControl.IsSingleTouchActive())
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);
            if (isGrounded)
            {
                if (rigidBody.velocity.magnitude > maxSpeed)
                {
                    rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
                }
                else
                {
                    Vector2 ballVector2 = touchControl.GetDeltaPos();
                    Vector3 ballVector3 = new Vector3(ballVector2.x, rigidBody.velocity.y, ballVector2.y);
                    rigidBody.AddForce(ballVector3 * speed, ForceMode.Force);
                }
            }
        }
    }
}

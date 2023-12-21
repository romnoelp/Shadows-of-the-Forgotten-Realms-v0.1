using System.Collections;
using UnityEngine;

namespace romnoelp
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D privateRigidBody2D;
        public Rigidbody2D rb 
        {
            get { return privateRigidBody2D; }
            private set { privateRigidBody2D = value; }
        }
        private float horizontalMovement;        
        public bool isFacingRight = true;
        private DirectionBias directionBias;

        [Header ("Movement")]
        [SerializeField] private float movementSpeed = 4f;
        [Header ("Camera Stuff")]
        [SerializeField] private GameObject cameraFollowObject;
        private float fallSpeedYDampingThreshold;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            directionBias = cameraFollowObject.GetComponent<DirectionBias>();
            fallSpeedYDampingThreshold = Manager.instance.fallSpeedYDampingChangeThreshold;
        }

        private void Update() 
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");

            if (rb.velocity.y < fallSpeedYDampingThreshold && !Manager.instance.isLerpingYDamping && 
            !Manager.instance.LerpedFromPlayerFalling)
            {
                Manager.instance.LerpYDamping(true);
            }
            if (rb.velocity.y >= 0f && !Manager.instance.isLerpingYDamping && Manager.instance.LerpedFromPlayerFalling)
            {
                Manager.instance.LerpedFromPlayerFalling = false;
                Manager.instance.LerpYDamping(false);
            }
        }

        // Physics for the movement
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
            FlipCheck();
        }

        // Flips the character when the horizontal movement becomes negative (Note: On a plane, negative <---- 0 ----> positive)
        private void Flip()
        {
            if (isFacingRight)
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                isFacingRight = !isFacingRight;
                directionBias.CallTurn();
            }
            else
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                isFacingRight = !isFacingRight;
                directionBias.CallTurn();
            }
        }

        private void FlipCheck()
        {
            if (horizontalMovement > 0f && !isFacingRight)
            {
                Flip();
            }
            else if (horizontalMovement < 0f && isFacingRight)
            {
                Flip();
            }
        }
    }    
}


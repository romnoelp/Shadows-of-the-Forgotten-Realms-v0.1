using System.Collections;
using UnityEngine;

namespace romnoelp
{
    public class Dash : MonoBehaviour
    {
        private Movement movementScript;
        private Rigidbody2D rb;
        private bool canDash = true;
        public bool getCanDash 
        {
            get { return canDash; }
            private set { canDash = value; }
        }
        private bool isDashing;
        public bool getIsDashing
        {
            get { return isDashing; }
            private set { isDashing = value; }
        }
        [SerializeField] private float dashForce = 24f;
        [SerializeField] private float dashDuration = .2f;
        [SerializeField] private float dashCooldown = 1f;
        [SerializeField] private TrailRenderer trailRenderer;

        private void Start() {
            movementScript = GetComponent<Movement>();
            rb = movementScript.rb;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(DashMovement());
            }
        }

        public IEnumerator DashMovement()
        {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2((movementScript.getIsFacingRight ? 1 : -1) * dashForce, 0f);
            trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashDuration);
            trailRenderer.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            Debug.Log("Dashed ");
        }
    }   
}


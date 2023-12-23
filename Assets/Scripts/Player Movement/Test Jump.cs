using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    public float jumpHeight = 5f;
    public float jumpDistance = 6f;

    void Update()
    {
        // Assuming a simple horizontal input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Calculate the jump trajectory using a parabolic equation
        float jumpProgress = (transform.position.x + jumpDistance) / (2 * jumpDistance);
        float jumpHeightAtX = -4 * jumpHeight / Mathf.Pow(jumpDistance, 2) * Mathf.Pow((transform.position.x - jumpDistance / 2), 2) + jumpHeight;

        // Apply the jump trajectory to the character's vertical position
        transform.position = new Vector3(transform.position.x + horizontalInput * Time.deltaTime, jumpHeightAtX, transform.position.z);
    }
}

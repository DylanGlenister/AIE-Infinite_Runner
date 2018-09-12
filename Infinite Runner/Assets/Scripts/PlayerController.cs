using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelController lController;
    public DeathController dController;

    // Keycodes used to store player controls
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode slide = KeyCode.LeftShift;

    // Player movement speeds
    public float groundMoveSpeed = 500f;
    public float groundMaxMoveSpeed = 15f;
    public float airMoveSpeed = 500f;
    public float airMaxMoveSpeed = 15f;

    // Player jump variables
    private bool isGrounded;
    public float jumpForce = 30f;
    public float jumpDelay = 0.1f;
    public float jumpTimer = 0.12f;
    public float maxRejumpDistance = 0.1f;

    // Player sliding variables
    private bool sliding = false;
    public float slideDelay = 0.1f;
    public float slideTimer = 0.12f;

    // Hit detection variables
    public bool leftWallHit = false;
    public bool rightWallHit = false;
    public float sideHitDetectionDistance = 0.06f;
    public float frontHitDetectionDistance = 3f;

    // A reference to the players rigidbody
    public Rigidbody rb;

    private void Awake()
    {
        lController = FindObjectOfType<LevelController>();
        dController = FindObjectOfType<DeathController>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //----------Raycasting----------

        // Tests if the player is in the air or not
        Ray groundDetectionRay = new Ray(transform.position + new Vector3(0, 0.01f, 0), new Vector3(0, -1, 0));
        //Debug.DrawLine(groundDetectionRay.origin, groundDetectionRay.origin + (Vector3.up * -maxRejumpDistance));
        RaycastHit groundDetectionRayHitInfo;
        if (Physics.Raycast(groundDetectionRay, out groundDetectionRayHitInfo, maxRejumpDistance) && groundDetectionRayHitInfo.collider.name.Contains("Floor"))
        {
            //Debug.Log("Found the ground!");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Sends a ray out to the left of the player to make sure they can't stick to a wall
        Ray leftWallRay = new Ray(transform.position + new Vector3(-0.49f, 0, 0), new Vector3(-1, 0, 0));
        //Debug.DrawLine(leftWallRay.origin, leftWallRay.origin + (-Vector3.right * sideHitDetectionDistance));
        RaycastHit leftWallRayHitInfo;
        if (Physics.Raycast(leftWallRay, out leftWallRayHitInfo, sideHitDetectionDistance) && leftWallRayHitInfo.collider.name.Contains("Wall"))
        {
            //Debug.Log("Left wall hit!");
            leftWallHit = true;
        }
        else
        {
            leftWallHit = false;
        }

        // Sends a ray out to the right of the player to make sure they can't stick to a wall
        Ray rightWallRay = new Ray(transform.position + new Vector3(0.49f, 0, 0), new Vector3(1, 0, 0));
        //Debug.DrawLine(rightWallRay.origin, rightWallRay.origin + (Vector3.right * sideHitDetectionDistance));
        RaycastHit rightWallRayHitInfo;
        if (Physics.Raycast(rightWallRay, out rightWallRayHitInfo, sideHitDetectionDistance) && rightWallRayHitInfo.collider.name.Contains("Wall"))
        {
            //Debug.Log("Right wall hit!");
            rightWallHit = true;
        }
        else
        {
            rightWallHit = false;
        }

        //----------Timers----------

        // Prevents the player from spamming the jump button
        if (jumpTimer <= jumpDelay)
            jumpTimer += Time.deltaTime;

        // Prevents the player from spamming the slide button
        if (sliding)
        {

        }

        //----------Player movement----------

        // Moves the player left
        if (Input.GetKey(moveLeft) && !leftWallHit && !dController.HasPlayerCollided())
        {
            if (isGrounded && rb.velocity.magnitude < groundMaxMoveSpeed)
            {
                rb.AddForce(new Vector3(-groundMoveSpeed, 0, 0));
            }
            else if (!isGrounded)
            {
                float LRValue = Vector3.Dot(transform.right, rb.velocity);
                if (rb.velocity.magnitude < airMaxMoveSpeed)
                {
                    rb.AddForce(new Vector3(-airMoveSpeed, 0, 0));
                }
                else
                {
                    if (LRValue > 0f)   // Moving right
                        rb.AddForce(new Vector3(-airMoveSpeed, 0, 0));
                }
            }
        }

        // Moves the player right
        if (Input.GetKey(moveRight) && !rightWallHit && !dController.HasPlayerCollided())
        {
            if (isGrounded && rb.velocity.magnitude < groundMaxMoveSpeed)
            {
                rb.AddForce(new Vector3(groundMoveSpeed, 0, 0));
            }
            else if (!isGrounded)
            {
                float LRValue = Vector3.Dot(transform.right, rb.velocity);
                if (rb.velocity.magnitude < airMaxMoveSpeed)
                {
                    rb.AddForce(new Vector3(airMoveSpeed, 0, 0));
                }
                else
                {
                    if (LRValue < 0f)   // Moving left
                        rb.AddForce(new Vector3(airMoveSpeed, 0, 0));
                }
            }
        }

        // Slows the player down if they hold both movement keys at once or if they are airborn
        if ((Input.GetKey(moveLeft) && Input.GetKey(moveRight)) || !isGrounded)
            rb.AddForce(new Vector3(-(rb.velocity.x * 2), 0, 0));

        // Makes the player jump
        if (Input.GetKey(jump) && jumpTimer >= 0.1f && isGrounded && !dController.HasPlayerCollided())
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            jumpTimer = 0f;
        }

        // Makes the player slide
        if (Input.GetKeyDown(slide) && slideTimer >= 0.1f && isGrounded && !dController.HasPlayerCollided())
        {
            sliding = true;
            // Play sliding animation
        }
        
        //Debug.Log(rb.velocity.magnitude);

        //----------Collision----------

        //Ray drawn on the left of the player to detect collsion between frames
        Ray rayHitDetectLeft = new Ray(transform.position + new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 1));
        Debug.DrawLine(rayHitDetectLeft.origin, rayHitDetectLeft.origin + (Vector3.forward * frontHitDetectionDistance));
        RaycastHit rayHitDetectLeftHitInfo;
        if (Physics.Raycast(rayHitDetectLeft, out rayHitDetectLeftHitInfo, frontHitDetectionDistance))
        {
            if (rayHitDetectLeftHitInfo.collider.name.Contains("Obstacle"))
            {
                //Debug.Log("Left Ray Hit");
                dController.ObstacleCollide();
            }
        }

        //Ray drawn on the right of the player to detect collsion between frames
        Ray rayHitDetectRight = new Ray(transform.position + new Vector3(0.5f, 0, 0), new Vector3(0, 0, 1));
        Debug.DrawLine(rayHitDetectRight.origin, rayHitDetectRight.origin + (Vector3.forward * frontHitDetectionDistance));
        RaycastHit rayHitDetectRightHitInfo;
        if (Physics.Raycast(rayHitDetectRight, out rayHitDetectRightHitInfo, frontHitDetectionDistance))
        {
            if (rayHitDetectRightHitInfo.collider.name.Contains("Obstacle"))
            {
                //Debug.Log("Right Ray Hit");
                dController.ObstacleCollide();
            }
        }

        if (dController.HasPlayerCollided())
            rb.isKinematic = true;
    }
}

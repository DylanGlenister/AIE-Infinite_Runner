using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References to other scripts
    private DeathController dController;

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
    public float jumpTimer = 0.0f;
    public float maxRejumpDistance = 0.1f;

    // Player sliding variables
    public bool standing = true;
    public float slideDelay = 0.66f;
    public float slideTimer = 0.0f;

    // Hit detection variables
    private bool leftWallHit = false;
    private bool rightWallHit = false;
    public float sideHitDetectionDistance = 0.06f;
    public float frontHitDetectionDistance = 3.5f;

    // A reference to the players rigidbody
    private Rigidbody rb;

    // A reference to the animator
    private Animator animator;

    private void Awake()
    {
        // Finds the scripts
        dController = FindObjectOfType<DeathController>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //----------Side and ground collision----------

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
        if (jumpTimer >= 0)
            jumpTimer -= Time.deltaTime;
        else
            jumpTimer = 0;

        // Prevents the player from spamming the slide button
        if (!standing)
        {
            if (slideTimer == 0)
            {
                standing = true;
            }
            else
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer < 0)
                    slideTimer = 0;
            }
        }

        //----------Player movement----------
        
        // Moves the player left
        if ((Input.GetKey(moveLeft) || Input.GetAxis("Horizontal") < 0) && !leftWallHit && !dController.HasPlayerCollided())
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
        if ((Input.GetKey(moveRight) || Input.GetAxis("Horizontal") > 0) && !rightWallHit && !dController.HasPlayerCollided())
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


        // Makes the player slide
        if ((Input.GetKey(slide) || Input.GetButton("Cancel")) && slideTimer == 0 && isGrounded && standing && !dController.HasPlayerCollided())
        {
            standing = false;
            slideTimer = slideDelay;
            // Play sliding animation
            animator.Play("SlideAnimation");
        }

        // Makes the player jump
        if ((Input.GetKey(jump) || Input.GetButton("Submit")) && jumpTimer == 0 && isGrounded && standing && !dController.HasPlayerCollided())
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            jumpTimer = jumpDelay;
        }

        if (dController.hasDied)
            animator.Play("PlayerStill");

        //Debug.Log(rb.velocity.magnitude);

        //----------Front Collision----------

        //Ray drawn on the bottom left of the player to detect collsion between frames
        Ray rayHitDetectBottomLeft = new Ray(transform.position + new Vector3(-0.5f, 0.5f, 0), new Vector3(0, 0, 1));
        //Debug.DrawLine(rayHitDetectBottomLeft.origin, rayHitDetectBottomLeft.origin + (Vector3.forward * frontHitDetectionDistance));
        RaycastHit rayHitDetectBottomLeftHitInfo;
        if (Physics.Raycast(rayHitDetectBottomLeft, out rayHitDetectBottomLeftHitInfo, frontHitDetectionDistance))
        {
            if (rayHitDetectBottomLeftHitInfo.collider.name.Contains("Obstacle"))
            {
                //Debug.Log("Left Ray Hit");
                dController.ObstacleCollide();
            }
        }

        //Ray drawn on the bottom right of the player to detect collsion between frames
        Ray rayHitDetectBottomRight = new Ray(transform.position + new Vector3(0.5f, 0.5f, 0), new Vector3(0, 0, 1));
        //Debug.DrawLine(rayHitDetectBottomRight.origin, rayHitDetectBottomRight.origin + (Vector3.forward * frontHitDetectionDistance));
        RaycastHit rayHitDetectBottomRightHitInfo;
        if (Physics.Raycast(rayHitDetectBottomRight, out rayHitDetectBottomRightHitInfo, frontHitDetectionDistance))
        {
            if (rayHitDetectBottomRightHitInfo.collider.name.Contains("Obstacle"))
            {
                //Debug.Log("Right Ray Hit");
                dController.ObstacleCollide();
            }
        }

        if (standing)
        {
            //Ray drawn on the top left of the player to detect collsion between frames
            Ray rayHitDetectTopLeft = new Ray(transform.position + new Vector3(-0.5f, 2.25f, 0), new Vector3(0, 0, 1));
            //Debug.DrawLine(rayHitDetectTopLeft.origin, rayHitDetectTopLeft.origin + (Vector3.forward * frontHitDetectionDistance));
            RaycastHit rayHitDetectTopLeftHitInfo;
            if (Physics.Raycast(rayHitDetectTopLeft, out rayHitDetectTopLeftHitInfo, frontHitDetectionDistance))
            {
                if (rayHitDetectTopLeftHitInfo.collider.name.Contains("Obstacle"))
                {
                    //Debug.Log("Left Ray Hit");
                    dController.ObstacleCollide();
                }
            }

            //Ray drawn on the top right of the player to detect collsion between frames
            Ray rayHitDetectTopRight = new Ray(transform.position + new Vector3(0.5f, 2.25f, 0), new Vector3(0, 0, 1));
            //Debug.DrawLine(rayHitDetectTopRight.origin, rayHitDetectTopRight.origin + (Vector3.forward * frontHitDetectionDistance));
            RaycastHit rayHitDetectTopRightHitInfo;
            if (Physics.Raycast(rayHitDetectTopRight, out rayHitDetectTopRightHitInfo, frontHitDetectionDistance))
            {
                if (rayHitDetectTopRightHitInfo.collider.name.Contains("Obstacle"))
                {
                    //Debug.Log("Right Ray Hit");
                    dController.ObstacleCollide();
                }
            }
        }

        if (dController.HasPlayerCollided())
            rb.isKinematic = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed, crouchSpeed, proneSpeed, walkingSpeed, slideSpeed, slideTimer, slideTimerMax, runningSpeed;
    public float gravity;
    public float jumpHeight;
    public float curVelocity, walkHeight, crouchHeight, proneHeight;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 velocity, slideFoward;
    public bool isGrounded, slideUnlocked, isSliding;

    public GameObject cam;
    public Vector3 crouchCam, normalCam;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);



        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        if ( isSliding == false && Input.GetButton("Slide"))
        {
            isSliding = true;
            slideTimer = 0f;
            slideFoward = transform.forward;
        }
        else if(isSliding == true)
        {
            controller.height = crouchHeight;
            speed = slideSpeed;
            controller.Move(slideFoward * slideSpeed * Time.deltaTime);

            slideTimer += Time.deltaTime;
            if (slideTimer > slideTimerMax)
            {
                isSliding = false;
            }
        }
        else if(isSliding == false)
        {
            controller.height = walkHeight;
            speed = walkingSpeed;
        }

        if(Input.GetButton("Run"))
        {
            speed = runningSpeed;
        }
        else if (Input.GetButton("Crouch") && isSliding == false)
        {
            controller.height = crouchHeight;
            speed = crouchSpeed;
        }
        else if (Input.GetButton("Prone") && isSliding == false)
        {
            cam.transform.localPosition = crouchCam;
            controller.height = proneHeight;
            controller.radius = .1f;
            speed = proneSpeed;
        }

        else if(isSliding == false)
        {
            cam.transform.localPosition = normalCam;
            controller.height = walkHeight;
            controller.radius = .3f;
            speed = walkingSpeed;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

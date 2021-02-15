using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed, crouchSpeed, proneSpeed, walkingSpeed, slideSpeed, slideTimer, slideTimerMax, runningSpeed;
    public float gravity;
    public float jumpHeight;
    public float curVelocity;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 velocity, slideFoward;
    public bool isGrounded, slideUnlocked, isSliding;

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
            controller.height = .5f;
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
            controller.height = 2f;
            speed = walkingSpeed;
        }

        if(Input.GetButton("Run"))
        {
            speed = runningSpeed;
        }
        else if (Input.GetButton("Crouch") && isSliding == false)
        {
            controller.height = 1.2f;
            speed = crouchSpeed;
        }
        else if (Input.GetButton("Prone") && isSliding == false)
        {
            controller.height = .3f;
            controller.radius = .3f;
            speed = proneSpeed;
        }

        else if(isSliding == false)
        {
            controller.height = 2f;
            controller.radius = .5f;
            speed = walkingSpeed;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

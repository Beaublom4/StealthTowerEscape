using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed, crouchSpeed, proneSpeed, walkingSpeed, slideSpeed, slideTimer, slideTimerMax, runningSpeed;
    public float curVelocity, walkHeight, crouchHeight, proneHeight;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 slideFoward;
    public bool slideUnlocked, isSliding, isJumping, canStandup, iscrouched, standUp;
    public float slideCooldown;
    public bool canSlide = true;

    public GameObject cam;
    public Vector3 crouchCam, normalCam;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move *= speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, .1f, groundMask, QueryTriggerInteraction.Ignore);
        if (!isGrounded)
            move.y = Physics.gravity.y;

        controller.Move(move * Time.deltaTime);

        if (canSlide && !isSliding && Input.GetButtonDown("Crouch") && Input.GetButton("Run"))
        {
            canSlide = false;
            isSliding = true;
            slideTimer = 0;
            slideFoward = transform.forward;
            anim.SetBool("isSliding", true);
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
                controller.height = walkHeight;
                speed = walkingSpeed;
                anim.SetBool("isSliding", false);
                Invoke("SlideCooldown", slideCooldown);
            }
        }
        if(isSliding && Input.GetButtonUp("Crouch") && Input.GetButtonUp("Run"))
        {
            isSliding = false;
            controller.height = walkHeight;
            speed = walkingSpeed;
            anim.SetBool("isSliding", false);
            Invoke("SlideCooldown", slideCooldown);
        }

        if (Input.GetButton("Run") && isSliding == false)
        {
            speed = runningSpeed;
            ResetAnims();
            anim.SetBool("isSprinting", true);
        }
        else if (Input.GetButton("Crouch") && isSliding == false)
        {
            controller.height = crouchHeight;
            speed = crouchSpeed;
            ResetAnims();
            anim.SetBool("isCrouched", true);
            iscrouched = true;
        }
        else if (Input.GetButton("Prone") && isSliding == false)
        {
            cam.transform.localPosition = crouchCam;
            controller.height = crouchHeight;
            controller.radius = .1f;
            speed = proneSpeed;
            anim.SetBool("isCrouched", true);
            iscrouched = true;
        }

        if (Input.GetButtonUp("Run"))
        {
            anim.SetBool("isSprinting", false);
            controller.height = walkHeight;
            speed = walkingSpeed;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            standUp = true;
        }
        if (Input.GetButtonUp("Prone"))
        {
            standUp = true;
        }

        if (iscrouched = true && canStandup == true && standUp == true)
        {
            anim.SetBool("isCrouched", false);
            cam.transform.localPosition = normalCam;
            controller.height = walkHeight;
            speed = walkingSpeed;
            iscrouched = false;
            standUp = false;
        }
    }
    void SlideCooldown()
    {
        canSlide = true;
    }
    void ResetAnims()
    {
        anim.SetBool("isSprinting", false);
        anim.SetBool("isCrouched", false);
        anim.SetBool("isSliding", false);
    }
}

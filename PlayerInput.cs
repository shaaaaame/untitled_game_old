using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerInput : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Combat combat;

    public static bool lockPos = false;

    private float vel;
    private bool jump;
    public bool interact;

    Vector3 posBeforeJump;
    public float maxJumpHeight;
    bool startJump = false;
    bool canJump = true;
    bool letGoOfJump = false;

    //in non-combat scene
    public static bool enableCombat = false;

    //for upwards attack
    public static bool lookingUp = false;

    void Update()
    {
        if (!lockPos)
        {
            if (canJump)
            {
                //jump input
                if (Input.GetButtonDown("Jump") && startJump)
                {
                    jump = true;
                    posBeforeJump = transform.position;
                    startJump = false;
                    letGoOfJump = false;
                }
                else if (Input.GetButtonDown("Jump"))
                {
                    jump = true;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    jump = false;
                    letGoOfJump = true;
                }
            }

            vel = Input.GetAxisRaw("Horizontal");
            if (vel != 0)
            {
                anim.SetBool("Walking", true);
            }
            else anim.SetBool("Walking", false);
        }
        else vel = 0;

        if (transform.position.y >= posBeforeJump.y + maxJumpHeight || letGoOfJump)
        {
            canJump = false;
            jump = false;
            startJump = true;
        }

        if (controller.isGrounded)
        {
            canJump = true;
        }

        //interaction input
        if (Input.GetButtonDown("Interact"))
        {
            interact = true;
        } 
        if (Input.GetButtonUp("Interact"))
        {
            interact = false;
        }

        //attack input
        if (enableCombat)
        {
            if (Input.GetButtonDown("Attack"))
            {
                combat.Attack();
            }

            //summono shield input
            if (Input.GetButtonDown("Shield"))
            {
                combat.SummonShield();
            }
        }


        //looking up
        if (Input.GetAxisRaw("Vertical") >= 0.5)
        {
            lookingUp = true;
        }
        else lookingUp = false;

    }

    void FixedUpdate()
    {

        controller.Move(vel, jump);
    }

    public static void LockPos()
    {
        lockPos = !lockPos;
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == 11 && interact)
        {
            other.GetComponent<NPC>().Trigger();
            interact = false;
        }
        else if (other.gameObject.layer == 12 && interact)
        {
            other.GetComponent<Interactable>().Interact();
            interact = false;
        }
        else if (other.gameObject.layer == 13 && interact)
        {
            other.GetComponent<SpecialNPC>().Trigger();
            interact = false;
        }
    }
}

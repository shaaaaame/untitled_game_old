using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Shield : MonoBehaviour
{
    public float speed = 1f;
    public Transform player;
    public Vector3[] positions;
    // position 0 : position when player facing left (2.3, 0.8, 0)
    // position 1 : position when player facing right (-2.3, 0.8, 0)
    // position 2 : position when attacking facing right (0.7, 0.2, 0) // default position when facing right
    // position 3 : position when attacking facing left (-0.7, 0.2, 0) // default position when facing left

    //for sprite swaps when moving back and forth between player
    public Animator anim;

    public float dist = 1f;

    public Combat combat;

    private void OnEnable()
    {
        if (CharacterController.FacingRight)
        {
            transform.position = player.position + positions[2];
        }
        else transform.position = player.position + positions[3];
    }


    private void FixedUpdate()
    {
        if (!Combat.inCombat)
        {
            // change position when facing left/right
            if (CharacterController.FacingRight)
            {
                transform.position = Vector3.Lerp(transform.position, player.position + positions[1], speed * Time.deltaTime * 10f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, player.position + positions[0], speed * Time.deltaTime * 10f);
            }

            // change sprite
            Vector3 vectorToPlayer = transform.position - player.position;
            if (vectorToPlayer.x <= dist && vectorToPlayer.x >= -dist)
            {
                anim.SetBool("Middle", true);
            }
            else if (vectorToPlayer.x < -1)
            {
                anim.SetBool("Middle", false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (vectorToPlayer.x > 1)
            {
                anim.SetBool("Middle", false);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        } else
        {
            if (CharacterController.FacingRight)
            {
                transform.position = player.position + positions[2];
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.position = player.position + positions[3];
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }


    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
